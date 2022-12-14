namespace Elsa.Core.Models.Account.Request;

/// <summary>
/// Запрос токена сброса пароля.
/// </summary>
public class ResetPasswordGetTokenRequest
{
    /// <summary>
    /// Почта.
    /// </summary>
    public required string Email { get; set; }
}
