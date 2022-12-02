namespace Elsa.API.Infrastructure.Projections.MinUser;

/// <summary>
/// Минимальная модель пользователя.
/// </summary>
class ElsaMinUser
{
    /// <summary>
    /// Id пользователя.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Роли пользователя.
    /// </summary>
    public ICollection<ElsaMinUserRole> UserRoles { get; set; }
}
