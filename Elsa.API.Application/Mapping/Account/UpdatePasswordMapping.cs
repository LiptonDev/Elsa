using AutoMapper;
using Elsa.API.Application.UseCases.Account.Commands.Update;
using Elsa.Core.Models.Account.Request;

namespace Elsa.API.Application.Mapping.Account;

/// <summary>
/// Mapping <see cref="ResetPasswordRequest"/> - <see cref="UpdateUserPasswordCommand"/>
/// </summary>
public class UpdatePasswordMapping : Profile
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public UpdatePasswordMapping()
    {
        CreateMap<ResetPasswordRequest, UpdateUserPasswordCommand>();
    }
}
