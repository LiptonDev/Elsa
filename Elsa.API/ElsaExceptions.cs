namespace Elsa.API;

/// <summary>
/// Локализация ошибок.
/// </summary>
class ElsaExceptions
{
    /// <summary>
    /// Ошибка валидации.
    /// </summary>
    public const string ValidationException = nameof(ValidationException);

    /// <summary>
    /// Объект не найден.
    /// </summary>
    public const string NotFoundException = nameof(NotFoundException);

    /// <summary>
    /// Необработанная ошибка.
    /// </summary>
    public const string UnhandledException = nameof(UnhandledException);
}
