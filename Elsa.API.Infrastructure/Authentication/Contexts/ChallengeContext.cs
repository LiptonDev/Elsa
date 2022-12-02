using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Elsa.API.Infrastructure.Authentication.Contexts;

public class ChallengeContext : PropertiesContext<ElsaSchemeOptions>
{
    public ChallengeContext(HttpContext context, AuthenticationScheme scheme, ElsaSchemeOptions options, AuthenticationProperties properties) : base(context, scheme, options, properties)
    {
    }

    public Exception? AuthenticateFailure { get; set; }

    public bool Handled { get; set; }
}
