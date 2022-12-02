namespace Elsa.Core.Models.Common;

/// <summary>
/// Identity error.
/// </summary>
public class ElsaIdentityError
{
    /// <summary>
    /// Код ошибки.
    /// </summary>
    public string ErrorCode { get; set; }

    /// <summary>
    /// Ошибки.
    /// </summary>
    public string[] Errors { get; set; }
}
