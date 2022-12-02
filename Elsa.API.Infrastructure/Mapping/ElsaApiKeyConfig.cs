using AutoMapper;
using Elsa.API.Infrastructure.Authentication;

namespace Elsa.API.Infrastructure.Mapping;

/// <summary>
/// Маппинг для API ключей.
/// </summary>
public class ElsaApiKeyConfig : Profile
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaApiKeyConfig()
    {
        CreateProjection<ElsaApiKey, ElsaMinApiKey>();
    }
}
