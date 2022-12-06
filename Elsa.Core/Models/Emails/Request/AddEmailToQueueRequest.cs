namespace Elsa.Core.Models.Emails.Request;

/// <summary>
/// Добавить письмо в очередь.
/// </summary>
public class AddEmailToQueueRequest
{
    /// <summary>
    /// Получатели.
    /// </summary>
    public List<string> Recipients { get; set; }

    /// <summary>
    /// Заголовок письма.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Тело письма.
    /// </summary>
    public string Body { get; set; }
}
