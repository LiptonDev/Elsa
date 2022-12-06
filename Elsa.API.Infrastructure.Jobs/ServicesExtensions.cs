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

            q.AddEmailRemoverJob();
            q.AddEmailSenderJob();
        });

        services.AddQuartzHostedService(x => x.WaitForJobsToComplete = true);
    }
}
