using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.API.Domain.Events.Account;
using Elsa.Core.Models.Account.Request;
using Elsa.Core.Models.Account.Response;

namespace Elsa.API.Application.UseCases.Account.Commands.Update;

/// <summary>
/// Команда смены пароля.
/// </summary>
public class UpdateUserPasswordCommand : ResetPasswordRequest, IElsaRequestWrapper<ResetPasswordResponse>
{
    /// <summary>
    /// <see langword="false"/> - пароль обновляет пользователь, иначе - администратор (токен сброса не нужен).
    /// </summary>
    public bool IsAdminAction { get; set; }
}

/// <summary>
/// Обработчик смены пароля пользователя.
/// </summary>
public class UpdateUserPasswordCommandHandler : IElsaRequestHandlerWrapper<UpdateUserPasswordCommand, ResetPasswordResponse>
{
    private readonly IAccountService accountService;
    private readonly IDomainEventsService publisher;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public UpdateUserPasswordCommandHandler(IAccountService accountService, IDomainEventsService publisher)
    {
        this.accountService = accountService;
        this.publisher = publisher;
    }

    /// <summary>
    /// Обработчик.
    /// </summary>
    /// <param name="request">Данные для смены пароля.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public async Task<ServiceResult<ResetPasswordResponse>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var (userId, res) = await accountService.ResetPasswordAsync(request, !request.IsAdminAction, cancellationToken);
        if (res.Succeeded)
        {
            await publisher.PublishAsync(new AccountPasswordChangedEvent(userId!), cancellationToken);
        }
        return new ServiceResult<ResetPasswordResponse>(res);
    }
}
