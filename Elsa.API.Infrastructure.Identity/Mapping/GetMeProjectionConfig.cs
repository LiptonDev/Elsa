using AutoMapper;
using Elsa.API.Infrastructure.Projections;
using Elsa.Core.Models.Account;

namespace Elsa.API.Infrastructure.Mapping;

/// <summary>
/// Маппинг для проекции пользователя.
/// </summary>
public class GetMeProjectionConfig : Profile
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public GetMeProjectionConfig()
    {
        CreateProjection<ElsaUser, GetMeProjection>();
        CreateMap<GetMeProjection, GetMeResponse>().ForMember(x => x.Roles, x => x.MapFrom(m => m.UserRoles.Select(x => x.Role.Name)));
    }
}
