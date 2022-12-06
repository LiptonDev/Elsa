using Elsa.API.Domain.Common;

namespace Elsa.API.Infrastructure;

/// <summary>
/// Ключ для API.
/// </summary>
public class ElsaApiKey : Entity<int>
{
    /// <summary>
    /// Ключ.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Пользователь, которому принадлежит ключ.
    /// </summary>
    public ElsaUser User { get; set; }

    /// <summary>
    /// Id пользователя.
    /// </summary>
    public string UserId { get; set; }
}
