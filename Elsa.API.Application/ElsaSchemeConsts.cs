namespace Elsa.API.Application;

/// <summary>
/// Контастанты для схемы авторизации.
/// </summary>
public class ElsaSchemeConsts
{
    /// <summary>
    /// Название схемы.
    /// </summary>
    public const string SchemeName = "ElsaScheme";

    /// <summary>
    /// Заголовок для ключа.
    /// </summary>
    public const string SchemeBearer = "Elsa-Token";

    /// <summary>
    /// Паттерн для ключа.
    /// </summary>
    public const string RegexPattern = $"^{TokenStart}[a-zA-Z0-9]{{128}}$";

    /// <summary>
    /// Начало токена.
    /// </summary>
    public const string TokenStart = "Elsa.";
}
