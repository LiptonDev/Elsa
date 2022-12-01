namespace Elsa.API.Application.Common.Models;

/// <summary>
/// Выход из системы (удаление токена).
/// </summary>
/// <param name="UserId">User Id.</param>
/// <param name="CurrentToken">Текущий токен (не указывается для удаления всех токенов).</param>
public record LogoutRequestModel();
