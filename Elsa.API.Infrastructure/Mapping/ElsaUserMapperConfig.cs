using AutoMapper;
using Elsa.API.Infrastructure.Projections.MinUser;

namespace Elsa.API.Infrastructure.Mapping;

/// <summary>
/// Маппинг для локального пользователя.
/// </summary>
public class ElsaUserMapperConfig : Profile
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaUserMapperConfig()
    {
        CreateProjection<ElsaUser, ElsaMinUser>();
        CreateMap<ElsaUserRole, ElsaMinUserRole>();
        CreateMap<ElsaRole, ElsaMinRole>();
    }
}
