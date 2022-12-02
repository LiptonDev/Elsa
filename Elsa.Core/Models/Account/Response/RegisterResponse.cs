using Elsa.Core.Models.Common;

namespace Elsa.Core.Models.Account.Response;

/// <summary>
/// Результат регистрации.
/// </summary>
public class RegisterResponse
{
    /// <summary>
    /// Результат регистрации.
    /// </summary>
    public bool Succeeded { get; set; }

    /// <summary>
    /// Ошибки регистрации.
    /// </summary>
    public List<ElsaIdentityError> Errors { get; set; }
}