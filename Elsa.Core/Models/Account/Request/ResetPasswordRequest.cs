namespace Elsa.Core.Models.Account.Request;

/// <summary>
/// Данные для обновления пароля.
/// </summary>
public class ResetPasswordRequest
{
    /// <summary>
    /// Почта.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Токен обнолвения пароля.
    /// </summary>
    public required string ResetToken { get; set; }

    /// <summary>
    /// Новый пароль.
    /// </summary>
    public required string NewPassword { get; set; }

    /// <summary>
    /// Подтверждение пароля.
    /// </summary>
    public required string ConfirmPassword { get; set; }
}
