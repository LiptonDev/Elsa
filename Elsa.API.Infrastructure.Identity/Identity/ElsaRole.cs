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
    public ElsaRole(string roleName, string description) : base(roleName)
    {
        UserRoles = new HashSet<ElsaUserRole>();
        Description = description;
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

    /// <summary>
    /// Описание роли.
    /// </summary>
    public string Description { get; set; }
}
