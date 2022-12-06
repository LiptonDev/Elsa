using Elsa.Core.Models.Common;

namespace Elsa.Core.Models.Account.Response;

/// <summary>
/// Результат регистрации.
/// </summary>
public class RegisterResponse : SucceededResponse
{
    /// <summary>
    /// Ошибки регистрации.
    /// </summary>
    public List<ElsaIdentityError> Errors { get; set; }
}