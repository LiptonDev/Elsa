using Elsa.API.Application.Common.Interfaces;

namespace Elsa.API.Infrastructure.Shared.Services;

/// <inheritdoc cref="IRandomStringGenerator" />
public class RandomStringGenerator : IRandomStringGenerator
{
    const string chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM123456789";

    public string Generate(int length)
    {
        return new string(Enumerable.Range(0, length).Select(x => chars[Random.Shared.Next(0, chars.Length)]).ToArray());
    }
}
