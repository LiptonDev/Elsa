using Microsoft.AspNetCore.Identity;

namespace Elsa.API.Infrastructure;

/// <summary>
/// Роль пользователя.
/// </summary>
public class ElsaRole : IdentityRole
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaRole(string roleName) : base(roleName)
    {
        UserRoles = new HashSet<ElsaUserRole>();
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaRole()
    {
        UserRoles = new HashSet<ElsaUserRole>();
    }

    /// <summary>
    /// Пользователи с ролью.
    /// </summary>
    public ICollection<ElsaUserRole> UserRoles { get; set; }
}
