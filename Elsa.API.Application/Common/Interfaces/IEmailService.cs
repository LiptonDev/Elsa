using Elsa.API.Application.Common.Models;

namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Сервис рассылки почты.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Отправить письмо.
    /// </summary>
    /// <param name="emaiRequest">Данные для отправки письма.</param>
    /// <returns></returns>
    Task SendAsync(EmailRequest emaiRequest);
}
