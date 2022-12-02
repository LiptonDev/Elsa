using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Domain.Entities;
using Elsa.API.Domain.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Elsa.API.Infrastructure.Shared.Services;

/// <inheritdoc cref="IEmailSenderService"/>
public class EmailSenderService : IEmailSenderService
{
    private readonly MailSettings settings;
    private readonly SmtpClient client;
    private readonly ILogger<EmailSenderService> logger;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public EmailSenderService(IOptions<MailSettings> options, ILogger<EmailSenderService> logger)
    {
        settings = options.Value;
        client = new SmtpClient();
        this.logger = logger;
    }

    public async Task<bool> SendAsync(EmailEntity emailRequest)
    {
        var email = new MimeMessage
        {
            Subject = emailRequest.Subject
        };
        email.From.Add(new MailboxAddress(settings.DisplayName, settings.EmailFrom));

        email.To.AddRange(emailRequest.ToAsEnumerable.Select(x => MailboxAddress.Parse(x)));

        var builder = new BodyBuilder
        {
            HtmlBody = emailRequest.Body
        };
        email.Body = builder.ToMessageBody();
        try
        {
            var ans = await client.SendAsync(email);
            logger.LogInformation($"Answer from smtp: {ans}");
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Sending email error");
            return false;
        }
    }

    public async Task<bool> StartAsync()
    {
        try
        {
            await client.ConnectAsync(settings.SmtpHost, settings.SmtpPort);
            await client.AuthenticateAsync(settings.SmtpUser, settings.SmtpPass);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Connection to SMTP error");
            return false;
        }
    }

    public async Task<bool> StopAsync()
    {
        try
        {
            await client.DisconnectAsync(true);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Disconnection from SMTP error");
            return false;
        }
    }
}
