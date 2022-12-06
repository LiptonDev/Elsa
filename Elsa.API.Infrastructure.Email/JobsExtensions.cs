using Elsa.API.Infrastructure.Email.Jobs;
using Quartz;

namespace Elsa.API.Infrastructure.Email;

public static class JobsExtensions
{
    /// <summary>
    /// Добавить задачу удаления отправленных писем.
    /// </summary>
    public static void AddEmailRemoverJob(this IServiceCollectionQuartzConfigurator configurator)
    {
        return;
        var jobKey = new JobKey("RemoveEmailJob");
        configurator.AddJob<DeleteSendedEmailsJob>(trigger =>
        {
            trigger.WithIdentity(jobKey);
            trigger.WithDescription("Удаление отправленных писем");
        });
        configurator.AddTrigger(x =>
        {
            x.ForJob(jobKey);
            x.WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever());
            x.WithIdentity("RemoveEmailJob-trigger");
        });
    }

    /// <summary>
    /// Добавить задачу рассылки писем.
    /// </summary>
    public static void AddEmailSenderJob(this IServiceCollectionQuartzConfigurator configurator)
    {
        var jobKey = new JobKey("SendEmailJob");
        configurator.AddJob<SendEmailJob>(trigger =>
        {
            trigger.WithIdentity(jobKey);
            trigger.WithDescription("Рассылка писем из базы");
        });
        configurator.AddTrigger(x =>
        {
            x.ForJob(jobKey);
            x.WithSimpleSchedule(x => x.WithIntervalInSeconds(15).RepeatForever());
            x.WithIdentity("SendEmailJob-trigger");
        });
    }
}
