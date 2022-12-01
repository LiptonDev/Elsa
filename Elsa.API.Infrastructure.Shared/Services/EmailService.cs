using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.API.Domain.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Elsa.API.Infrastructure.Shared.Services;

/// <inheritdoc cref="IEmailService"/>
public class EmailService : IEmailService
{
    private readonly MailSettings settings;
    private readonly ILogger<EmailService> logger;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public EmailService(IOptions<MailSettings> options, ILogger<EmailService> logger)
    {
        settings = options.Value;
        this.logger = logger;
    }

    public async Task SendAsync(EmailRequest emailRequest)
    {
        var email = new MimeMessage
        {
            Subject = emailRequest.Subject
        };
        email.From.Add(new MailboxAddress(settings.DisplayName, settings.EmailFrom));

        email.To.AddRange(emailRequest.To.Select(x => MailboxAddress.Parse(x)));

        var builder = new BodyBuilder
        {
            HtmlBody = emailRequest.Body
        };
        email.Body = builder.ToMessageBody();
        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(settings.SmtpHost, settings.SmtpPort);
            await client.AuthenticateAsync(settings.SmtpUser, settings.SmtpPass);
            await client.SendAsync(email);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Send email error");
        }
    }
}
