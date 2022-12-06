using Elsa.API.Application.UseCases.Account.Commands.Create;
using Elsa.API.Application.UseCases.Account.Commands.Delete;
using Elsa.API.Application.UseCases.Account.Queries.GetToken;
using Elsa.Core.Models.Account.Request;
using Elsa.Core.Models.Account.Response;

namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Сервис аккаунтов.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Сброс пароля.
    /// </summary>
    /// <param name="request">Данные для сброса пароля.</param>
    /// <param name="useRequestToken"><see langword="true"/> - использовать токен из запроса, иначе сгенерировать новый и сразу использовать его.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>
    /// Строка - ID пользователя (<see langword="null"/> если не найден).
    /// </returns>
    Task<(string?, ResetPasswordResponse)> ResetPasswordAsync(ResetPasswordRequest request, bool useRequestToken, CancellationToken cancellationToken);

    /// <summary>
    /// Отправить токен сброса пароля.
    /// </summary>
    /// <param name="userId">Id пользователя, для которого будет отправлен токен сброса пароля.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    Task<ResetPasswordGetTokenResponse> SendResetPasswordTokenAsync(string userId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить информацию о пользователях.
    /// </summary>
    /// <param name="userIds">Id'ы пользователей.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    Task<List<GetMeResponse>> GetUsersInfoAsync(string[] userIds, CancellationToken cancellationToken);

    /// <summary>
    /// Регистрация пользователя.
    /// </summary>
    /// <param name="request">Данные для регистрации.</param>
    /// <returns>
    /// <para>bool - статус регистрации (true - успешно).</para>
    /// <para>string - первая ошибка регистрации.</para>
    /// </returns>
    Task<RegisterResponse> RegisterAsync(CreateUserCommand request);

    /// <summary>
    /// Попытка авторизации.
    /// </summary>
    /// <param name="request">Данные для авторизации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    Task<LoginResponse?> TryLoginAsync(GetAccessTokenCommand request, CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет, используется ли заданная почта у какого-то аккаунта.
    /// </summary>
    /// <param name="email">Почта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see cref="bool">true</see> если почта используется.</returns>
    Task<bool> CheckEmailAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить токен(ы) авторизации.
    /// </summary>
    /// <param name="request">Данные для удаления токен(а/ов).</param>
    /// <param name="cancellationToken">Токен отмены задания.</param>
    /// <returns>Количество удаленных токенов.</returns>
    Task<string[]> RemoveTokenAsync(LogoutCommand request, CancellationToken cancellationToken);
}
