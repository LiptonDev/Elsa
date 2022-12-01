namespace Elsa.Core.Enums;

/// <summary>
/// Коды ошибок.
/// </summary>
public enum ErrorCode
{
    /// <summary>
    /// Необработанная ошибки.
    /// </summary>
    Unhandled = -1,

    /// <summary>
    /// Внутреняя ошибка API.
    /// </summary>
    Internal = 1,

    /// <summary>
    /// Ошибка валидации.
    /// </summary>
    Validation = 2,

    /// <summary>
    /// Пользователь не авторизован.
    /// </summary>
    Unauthorized = 3,

    /// <summary>
    /// Значение не найдено.
    /// </summary>
    ValueNotFound = 4,
}
