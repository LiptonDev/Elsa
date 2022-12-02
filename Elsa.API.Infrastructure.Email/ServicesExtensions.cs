using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Elsa.API.Infrastructure.Email;

public static class ServicesExtensions
{
    /// <summary>
    /// Добавить сервисы для работы с почтой.
    /// </summary>
    /// <param name="services"></param>
    public static void AddEmailInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IEmailSenderService, EmailSenderService>();
    }
}
