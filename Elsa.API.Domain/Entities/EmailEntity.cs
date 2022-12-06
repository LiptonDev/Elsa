using Elsa.API.Domain.Common;

namespace Elsa.API.Domain.Entities;

/// <summary>
/// Письмо.
/// </summary>
public class EmailEntity : Entity<int>
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
    /// Следующая дата и время попытки отправить письмо.
    /// </summary>
    public DateTime? NextTrySend { get; set; }

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

    /// <summary>
    /// Статус отправки письма.
    /// </summary>
    public bool Sended { get; set; }

    /// <summary>
    /// Временной флаг для обработки SyntaxError (неправильный адрес).
    /// </summary>
    public bool Fail { get; set; }
}
