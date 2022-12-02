using Elsa.API.Application;
using Elsa.API.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elsa.API.Infrastructure.Shared.Services;

/// <inheritdoc cref="ITokenGenerator"/>
public class TokenGenerator : ITokenGenerator
{
    private readonly IRandomStringGenerator stringGenerator;
    private readonly IUnitOfWork<int> unitOfWork;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public TokenGenerator(IRandomStringGenerator stringGenerator, IUnitOfWork<int> unitOfWork)
    {
        this.stringGenerator = stringGenerator;
        this.unitOfWork = unitOfWork;
    }

    public async Task<string> GenerateTokenAsync()
    {
        var token = $"{ElsaSchemeConsts.TokenStart}{stringGenerator.Generate(128)}";
        var contains = await unitOfWork.Repository<ElsaApiKey>().Entities.AnyAsync(x => x.Key == token);
        if (contains)
        {
            return await GenerateTokenAsync();
        }
        else
        {
            return token;
        }
    }
}
