using Elsa.Core.Models.Common;

namespace Elsa.Core.Models.Account.Response;

/// <summary>
/// Ответ об изменении пароля.
/// </summary>
public class ResetPasswordResponse : SucceededResponse
{
    /// <summary>
    /// Ошибки.
    /// </summary>
    public List<ElsaIdentityError> Errors { get; set; }
}
