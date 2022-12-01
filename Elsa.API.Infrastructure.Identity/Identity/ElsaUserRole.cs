using Microsoft.AspNetCore.Identity;

namespace Elsa.API.Infrastructure;

/// <summary>
/// Роли пользователя.
/// </summary>
public class ElsaUserRole : IdentityUserRole<string>
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public ElsaUser User { get; set; }

    /// <summary>
    /// Роль.
    /// </summary>
    public ElsaRole Role { get; set; }
}
