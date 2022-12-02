using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Elsa.API.Infrastructure.Email.Jobs;

/// <summary>
/// Задача рассылки писем.
/// </summary>
public class SendEmailJob : IJob
{
    private readonly IEmailSenderService sender;
    private readonly IUnitOfWork<int> unitOfWork;
    private readonly IDateTimeService dateTimeService;
    private readonly ILogger<SendEmailJob> logger;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public SendEmailJob(IEmailSenderService sender, IUnitOfWork<int> unitOfWork, IDateTimeService dateTimeService, ILogger<SendEmailJob> logger)
    {
        this.sender = sender;
        this.unitOfWork = unitOfWork;
        this.dateTimeService = dateTimeService;
        this.logger = logger;
    }

    /// <summary>
    /// Выполнение задачи.
    /// </summary>
    public async Task Execute(IJobExecutionContext context)
    {
        var mails = await unitOfWork.Repository<EmailEntity>().Entities.OrderBy(x => x.CreatedOn).Take(25).ToListAsync(context.CancellationToken);

        if (mails.Count < 1)
        {
            logger.LogInformation("Not found mails for sending");
            return;
        }

        var start = await sender.StartAsync();
        if (!start)
            return;

        foreach (var item in mails)
        {
            var res = await sender.SendAsync(item);
            if (!res)
            {
                item.Fails++;
            }
            else
            {
                item.IsDeleted = true;
                item.DeletedOn = dateTimeService.UtcNow;
            }
        }
        await sender.StopAsync();
        await unitOfWork.Commit(context.CancellationToken);
    }
}
