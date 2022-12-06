namespace Elsa.API.Infrastructure.Projections.MinUser;

/// <summary>
/// Минимальная модель пользователя.
/// </summary>
class ElsaMinUser
{
    /// <summary>
    /// Id пользователя.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Роли пользователя.
    /// </summary>
    public required ICollection<ElsaMinUserRole> UserRoles { get; set; }
}
