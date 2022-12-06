using AutoMapper;
using Elsa.API.Application.UseCases.Account.Commands.Delete;
using Elsa.Core.Models.Account.Request;

namespace Elsa.API.Application.Mapping.Account;

/// <summary>
/// Маппинг для выхода.
/// </summary>
public class LogoutMapping : Profile
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public LogoutMapping()
    {
        CreateMap<LogoutRequest, LogoutCommand>();
        CreateMap<DeleteUserTokensRequest, LogoutCommand>();
    }
}
