namespace Elsa.Core.Models.Account;

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
    public List<RegisterError> Errors { get; set; }
}

/// <summary>
/// Ошибки регистрации.
/// </summary>
public class RegisterError
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