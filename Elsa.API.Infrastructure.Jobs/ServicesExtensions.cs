using Elsa.API.Infrastructure.Email.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Elsa.API.Infrastructure.Email;

public static class ServicesExtensions
{
    /// <summary>
    /// Добавить службу заданий.
    /// </summary>
    /// <param name="services"></param>
    public static void AddQuartzJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            var jobKey = new JobKey("SendEmailJob");
            q.AddJob<SendEmailJob>(trigger =>
            {
                trigger.WithIdentity(jobKey);
                trigger.WithDescription("Рассылка писем из базы");
            });
            q.AddTrigger(x =>
            {
                x.ForJob(jobKey);
                x.WithSimpleSchedule(x => x.WithIntervalInSeconds(15).RepeatForever());
                x.WithIdentity("SendEmailJob-trigger");
            });
        });

        services.AddQuartzHostedService(x => x.WaitForJobsToComplete = true);
    }
}
