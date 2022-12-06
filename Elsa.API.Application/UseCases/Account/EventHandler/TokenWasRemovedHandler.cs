using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.API.Domain.Events.Account;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Elsa.API.Application.UseCases.Account.EventHandler;

/// <summary>
/// Обработчик события удаления токена пользователя.
/// </summary>
public class TokenWasRemovedHandler : INotificationHandler<DomainEventNotification<TokensWasRemovedEvent>>
{
    private readonly IDistributedCache cache;
    private readonly IHubDisconnector hubDisconnecter;
    private readonly ILogger<TokenWasRemovedHandler> logger;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public TokenWasRemovedHandler(IDistributedCache cache, IHubDisconnector hubDisconnecter, ILogger<TokenWasRemovedHandler> logger)
    {
        this.cache = cache;
        this.hubDisconnecter = hubDisconnecter;
        this.logger = logger;
    }

    /// <summary>
    /// Обработчик.
    /// </summary>
    /// <param name="notification">Событие.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task Handle(DomainEventNotification<TokensWasRemovedEvent> notification, CancellationToken cancellationToken)
    {
        RemoveTokens(notification.DomainEvent.Tokens);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Асинхронно удаляет токены из Redis.
    /// </summary>
    /// <param name="tokens">Токены, которые были удалены.</param>
    private async void RemoveTokens(string[] tokens)
    {
        foreach (var item in tokens)
        {
            await cache.RemoveAsync($"{ElsaSchemeConsts.SchemeBearer}={item}");
            await hubDisconnecter.DisconnectAsync(item);
        }
        logger.LogInformation("Removed {count} tokens from Redis", tokens.Length);
    }
}