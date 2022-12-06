using Elsa.API.Application;
using Elsa.API.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elsa.API.Infrastructure.Shared.Services;

/// <inheritdoc cref="ITokenGenerator"/>
public class TokenGenerator : ITokenGenerator
{
    private readonly IRandomStringGenerator stringGenerator;
    private readonly IAsyncRepository<ElsaApiKey, int> repository;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public TokenGenerator(IRandomStringGenerator stringGenerator, IAsyncRepository<ElsaApiKey, int> repository)
    {
        this.stringGenerator = stringGenerator;
        this.repository = repository;
    }

    public async Task<string> GenerateTokenAsync(CancellationToken cancellationToken)
    {
        var token = $"{ElsaSchemeConsts.TokenStart}{stringGenerator.Generate(128)}";
        var contains = await repository.Entities().AnyAsync(x => x.Key == token, cancellationToken);
        if (contains)
        {
            return await GenerateTokenAsync(cancellationToken);
        }
        else
        {
            return token;
        }
    }
}
