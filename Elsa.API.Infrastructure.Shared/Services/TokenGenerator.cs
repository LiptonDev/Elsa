using Elsa.API.Application;
using Elsa.API.Application.Common.Interfaces;

namespace Elsa.API.Infrastructure.Shared.Services;

/// <inheritdoc cref="ITokenGenerator"/>
public class TokenGenerator : ITokenGenerator
{
    private readonly IRandomStringGenerator stringGenerator;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public TokenGenerator(IRandomStringGenerator stringGenerator)
    {
        this.stringGenerator = stringGenerator;
    }

    public string GenerateToken()
    {
        return $"{ElsaSchemeConsts.TokenStart}{stringGenerator.Generate(128)}";
    }
}
