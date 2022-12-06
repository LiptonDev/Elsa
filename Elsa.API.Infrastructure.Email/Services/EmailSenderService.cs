using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Domain.Entities;
using Elsa.API.Domain.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text.RegularExpressions;

namespace Elsa.API.Infrastructure.Shared.Services;

/// <inheritdoc cref="IEmailSenderService"/>
public partial class EmailSenderService : IEmailSenderService
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

    public async Task<SmtpStatusCode?> SendAsync(EmailEntity emailRequest, CancellationToken cancellationToken)
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
            var ans = await client.SendAsync(email, cancellationToken);
            return SmtpStatusCode.Ok;
        }
        catch (SmtpCommandException ex)
        {
            if (ex.StatusCode != SmtpStatusCode.SyntaxError)//syntax error не интересен
            {
                logger.LogWarning("Send mail error: status_code: {status}, code: {code}, text: {text}", ex.StatusCode, ex.ErrorCode, ex.Message);
            }
            return ex.StatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Sending email error");
            return null;
        }
    }

    public async Task<bool> StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await client.ConnectAsync(settings.SmtpHost, settings.SmtpPort, settings.UseSsl, cancellationToken);
            await client.AuthenticateAsync(settings.SmtpUser, settings.SmtpPass, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Connection to SMTP error");
            return false;
        }
    }

    public async Task<bool> StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            await client.DisconnectAsync(true, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Disconnection from SMTP error");
            return false;
        }
    }
}
