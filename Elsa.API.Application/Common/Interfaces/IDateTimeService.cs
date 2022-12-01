namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Сервис текущей даты и времени.
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    /// Текущая дата и время.
    /// </summary>
    DateTime UtcNow { get; }
}
