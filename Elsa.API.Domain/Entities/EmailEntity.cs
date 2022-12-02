using Elsa.API.Domain.Common;

namespace Elsa.API.Domain.Entities;

/// <summary>
/// Письмо.
/// </summary>
public class EmailEntity : AuditableEntity<int>
{
    /// <summary>
    /// Получатели (через запятую).
    /// </summary>
    public string To { get; set; }

    /// <summary>
    /// Получатели письма.
    /// </summary>
    public IEnumerable<string> ToAsEnumerable => To.Split(',');

    /// <summary>
    /// Заголовок письма.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Тело письма.
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Количество ошибок при отправке сообщения.
    /// </summary>
    public int Fails { get; set; }
}
