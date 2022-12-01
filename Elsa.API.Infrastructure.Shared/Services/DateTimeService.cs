using Elsa.API.Application.Common.Interfaces;

namespace Elsa.API.Infrastructure.Shared.Services;

/// <inheritdoc cref="IDateTimeService"/>
public class DateTimeService : IDateTimeService
{
    public DateTime UtcNow { get; } = DateTime.UtcNow;
}
