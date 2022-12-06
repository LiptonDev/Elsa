using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Elsa.API.Infrastructure.Email.Jobs;

/// <summary>
/// Задача удаления отправленных писем (либо тех, которые не удалось отправить несколько раз).
/// </summary>
public class DeleteSendedEmailsJob : IJob
{
    private readonly IAsyncRepository<EmailEntity, int> repository;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public DeleteSendedEmailsJob(IAsyncRepository<EmailEntity, int> repository)
    {
        this.repository = repository;
    }

    /// <summary>
    /// Выполнение задачи.
    /// </summary>
    public Task Execute(IJobExecutionContext context)
    {
        return repository.Entities().IgnoreQueryFilters().Where(x => x.Fails >= 3 || x.Sended).ExecuteDeleteAsync(context.CancellationToken);
    }
}
