namespace Elsa.Core.Models.Account.Request;

/// <summary>
/// Данные для обновления пароля.
/// </summary>
public class ResetPasswordRequest
{
    /// <summary>
    /// Почта.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Токен обнолвения пароля.
    /// </summary>
    public string ResetToken { get; set; }

    /// <summary>
    /// Новый пароль.
    /// </summary>
    public string NewPassword { get; set; }

    /// <summary>
    /// Подтверждение пароля.
    /// </summary>
    public string ConfirmPassword { get; set; }
}
