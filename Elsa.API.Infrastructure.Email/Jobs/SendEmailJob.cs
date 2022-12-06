using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Domain.Entities;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Elsa.API.Infrastructure.Email.Jobs;

/// <summary>
/// Задача рассылки писем.
/// </summary>
public class SendEmailJob : IJob
{
    private readonly IEmailSenderService sender;
    private readonly IAsyncRepository<EmailEntity, int> repository;
    private readonly IDateTimeService dateTimeService;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public SendEmailJob(IEmailSenderService sender, IAsyncRepository<EmailEntity, int> repository, IDateTimeService dateTimeService)
    {
        this.sender = sender;
        this.repository = repository;
        this.dateTimeService = dateTimeService;
    }

    /// <summary>
    /// Выполнение задачи.
    /// </summary>
    public async Task Execute(IJobExecutionContext context)
    {
        var mails = await repository.Entities().Where(x => !x.NextTrySend.HasValue || x.NextTrySend <= dateTimeService.UtcNow).OrderBy(x => x.Id).Take(25).ToListAsync(context.CancellationToken);
        if (!mails.Any())
        {
            return;
        }

        var start = await sender.StartAsync(context.CancellationToken);
        if (!start)
            return;

        foreach (var item in mails)
        {
            var result = await sender.SendAsync(item, context.CancellationToken);

            switch (result)
            {
                case SmtpStatusCode.Ok:
                    item.Sended = true;
                    break;

                case SmtpStatusCode.SyntaxError:
                    item.Fail = true;
                    break;

                case null:
                    await sender.StopAsync(context.CancellationToken);
                    return; //какая-то магия со стороны Smtp.
            }
        }
        await sender.StopAsync(context.CancellationToken);
        var ids = new
        {
            sended = mails.Where(x => x.Sended).Select(x => x.Id),
            notSended = mails.Where(m => !m.Sended).Select(m => m.Id),
            fails = mails.Where(m => m.Fail).Select(m => m.Id)
        };

        if (ids.sended.Any())
        {
            await repository.Entities()
                            .Where(x => ids.sended.Contains(x.Id))
                            .ExecuteUpdateAsync(x => x.SetProperty(p => p.Sended, p => true));
        }
        if (ids.fails.Any() || ids.notSended.Any())
        {
            await repository.Entities()
                            .Where(x => ids.notSended.Contains(x.Id))
                            .ExecuteUpdateAsync(x => x.SetProperty(p => p.Fails, p => ids.fails.Contains(p.Id) ? 3 : p.Fails + 1) //Если пришел ответ SyntaxError, то сразу установить кол-во попыток = 3
                                                      .SetProperty(p => p.NextTrySend, p => dateTimeService.UtcNow.AddMinutes(5))); //У всех обновить следующую попытку отправить письмо через 5 минут
        }
    }
}
