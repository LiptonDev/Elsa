namespace Elsa.Core.Models.Account.Request;

/// <summary>
/// Запрос токена сброса пароля.
/// </summary>
public class ResetPasswordGetTokenRequest
{
    /// <summary>
    /// Почта.
    /// </summary>
    public string Email { get; set; }
}
