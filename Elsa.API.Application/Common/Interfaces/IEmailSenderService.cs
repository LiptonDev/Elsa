using Elsa.API.Domain.Entities;

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
    /// <see langword="true"/> - письмо отправлено.
    /// </returns>
    Task<bool> SendAsync(EmailEntity emaiRequest);

    /// <summary>
    /// Открыть подключение к SMTP.
    /// </summary>
    /// <returns></returns>
    Task<bool> StartAsync();

    /// <summary>
    /// Закрыть подключение к SMTP.
    /// </summary>
    /// <returns></returns>
    Task<bool> StopAsync();
}
