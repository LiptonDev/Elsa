namespace Elsa.API.Application.Common.Models;

/// <summary>
/// Письмо.
/// </summary>
public class EmailRequest
{
    /// <summary>
    /// Получатели письма.
    /// </summary>
    public List<string> To { get; set; }

    /// <summary>
    /// Заголовок письма.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Тело письма.
    /// </summary>
    public string Body { get; set; }
}
