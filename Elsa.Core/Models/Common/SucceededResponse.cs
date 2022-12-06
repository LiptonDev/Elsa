namespace Elsa.Core.Models.Common;

/// <summary>
/// Ответ, содержащий <see cref="bool"/> ответ.
/// </summary>
public class SucceededResponse
{
    /// <summary>
    /// <see langword="true"/> - успешный запрос.
    /// </summary>
    public required bool Succeeded { get; set; }
}
