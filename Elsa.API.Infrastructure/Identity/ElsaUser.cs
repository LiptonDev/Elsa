using Microsoft.AspNetCore.Identity;

namespace Elsa.API.Infrastructure;

/// <summary>
/// Пользователь Elsa.
/// </summary>
public class ElsaUser : IdentityUser
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaUser()
    {
        UserRoles = new HashSet<ElsaUserRole>();
        ApiKeys = new HashSet<ElsaApiKey>();
    }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Ключи авторизации.
    /// </summary>
    public ICollection<ElsaApiKey> ApiKeys { get; set; }


    /// <summary>
    /// Роли.
    /// </summary>
    public ICollection<ElsaUserRole> UserRoles { get; set; }
}
