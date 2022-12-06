using AutoMapper;
using Elsa.API.Application;
using Elsa.API.Infrastructure.Authentication.Contexts;
using Elsa.API.Infrastructure.Persistence;
using Elsa.API.Infrastructure.Projections.MinUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Elsa.API.Infrastructure.Authentication;

/// <summary>
/// Thx Microsoft :)
/// </summary>
file class MyClaim : Claim
{
    public MyClaim(string type, string value, string valueType, string issuer, string originalIssuer) :
        base(type, value, valueType, issuer, originalIssuer)
    { }
}

/// <summary>
/// Обработчик авторизации.
/// </summary>
public class ElsaSchemeHandler : AuthenticationHandler<ElsaSchemeOptions>
{
    private readonly ElsaDbContext dbContext;
    private readonly IDistributedCache redisCache;
    private readonly IMapper mapper;
    static readonly Regex tokenRegex = new(ElsaSchemeConsts.RegexPattern, RegexOptions.Compiled);

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaSchemeHandler(IOptionsMonitor<ElsaSchemeOptions> options,
                             ILoggerFactory logger,
                             UrlEncoder encoder,
                             ISystemClock clock,
                             ElsaDbContext dbContext,
                             IDistributedCache redisCache,
                             IMapper mapper) : base(options, logger, encoder, clock)
    {
        this.dbContext = dbContext;
        this.redisCache = redisCache;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    protected new ElsaSchemeEvents Events
    {
        get => (ElsaSchemeEvents)base.Events!;
        set => base.Events = value;
    }

    /// <inheritdoc/>
    protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        var context = new ForbiddenContext(Context, Scheme, Options);
        Response.StatusCode = 403;
        return Events.OnForbidden(context);
    }

    /// <inheritdoc/>
    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        var authResult = await HandleAuthenticateOnceSafeAsync();
        var eventContext = new ChallengeContext(Context, Scheme, Options, properties)
        {
            AuthenticateFailure = authResult?.Failure
        };

        Response.StatusCode = 401;
        await Events.OnChallenge(eventContext);
    }

    /// <summary>
    /// Обработчик авторизации.
    /// </summary>
    /// <returns></returns>
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            if (!Request.Headers.ContainsKey(ElsaSchemeConsts.SchemeBearer))
            {
                return AuthenticateResult.NoResult();
            }

            var headerKey = Request.Headers[ElsaSchemeConsts.SchemeBearer].ToString();

            if (!tokenRegex.IsMatch(headerKey))
            {
                return AuthenticateResult.NoResult();
            }

            var redisKey = await redisCache.GetStringAsync($"{ElsaSchemeConsts.SchemeBearer}={headerKey}");
            if (redisKey is not null)
            {
                var parse = JsonSerializer.Deserialize<IEnumerable<MyClaim>>(redisKey);
                var claimsIdentity = new ClaimsIdentity(parse, nameof(ElsaSchemeHandler));
                var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            else
            {
                var key = await mapper.ProjectTo<ElsaMinApiKey>(dbContext.ApiKeys.AsNoTracking()).FirstOrDefaultAsync(x => x.Key == headerKey);

                if (key == null)
                {
                    return AuthenticateResult.Fail("Key not found");
                }

                var res = await mapper.ProjectTo<ElsaMinUser>(dbContext.Users.AsNoTracking()).FirstOrDefaultAsync(x => x.Id == key.UserId);

                if (res == null)
                {
                    return AuthenticateResult.Fail("User not found");
                }

                var claims = GetClaims(res);
                var claimsIdentity = new ClaimsIdentity(claims, nameof(ElsaSchemeHandler));
                var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);

                var json = JsonSerializer.Serialize(claims);
                await redisCache.SetStringAsync($"{ElsaSchemeConsts.SchemeBearer}={headerKey}", json);

                return AuthenticateResult.Success(ticket);
            }
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex.Message);
        }
    }

    /// <summary>
    /// Получить список всех доступов для пользователя.
    /// </summary>
    private static IEnumerable<Claim> GetClaims(ElsaMinUser user)
    {
        return user.UserRoles.Select(x => new Claim(ClaimTypes.Role, x.Role.Name)).Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
    }
}

/// <summary>
/// Минимальная модель для API ключей.
/// </summary>
class ElsaMinApiKey
{
    /// <summary>
    /// Id пользователя.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Ключ.
    /// </summary>
    public required string Key { get; set; }
}