namespace Elsa.Core.Models.Account.Response;

/// <summary>
/// Jwt token.
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// Токен авторизации.
    /// </summary>
    public string ApiToken { get; set; }
}
