using Elsa.Core.Models.Common;

namespace Elsa.Core.Models.Account.Response;

/// <summary>
/// Ответ об изменении пароля.
/// </summary>
public class ResetPasswordResponse
{
    /// <summary>
    /// Результат сброса пароля.
    /// </summary>
    public bool Succeeded { get; set; }

    /// <summary>
    /// Сообщение от API.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Ошибки.
    /// </summary>
    public List<ElsaIdentityError> Errors { get; set; }
}
