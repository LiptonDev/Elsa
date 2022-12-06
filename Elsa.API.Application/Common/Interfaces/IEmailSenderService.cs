using Elsa.API.Domain.Entities;
using MailKit.Net.Smtp;

namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Сервис рассылки почты.
/// </summary>
public interface IEmailSenderService
{
    /// <summary>
    /// Отправить письмо.
    /// </summary>
    /// <param name="emaiRequest">Данные для отправки письма.</param>
    /// <returns>
    /// <see langword="null"/> - необработанная ошибка.
    /// </returns>
    Task<SmtpStatusCode?> SendAsync(EmailEntity emaiRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Открыть подключение к SMTP.
    /// </summary>
    /// <returns></returns>
    Task<bool> StartAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Закрыть подключение к SMTP.
    /// </summary>
    /// <returns></returns>
    Task<bool> StopAsync(CancellationToken cancellationToken);
}
