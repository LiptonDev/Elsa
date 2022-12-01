namespace Elsa.Core.Enums;

/// <summary>
/// Тип удаления токена.
/// </summary>
public enum RemoveTokenType
{
    /// <summary>
    /// Удалить только текущий токен.
    /// </summary>
    JustCurrent = 1000,

    /// <summary>
    /// Удалить все токены, кроме текущего.
    /// </summary>
    AllExceptCurrent = 2000,

    /// <summary>
    /// Удалить все токены.
    /// </summary>
    All = -1,
}
