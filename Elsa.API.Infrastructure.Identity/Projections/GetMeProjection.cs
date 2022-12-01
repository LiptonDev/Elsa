using Elsa.API.Infrastructure.Projections.MinUser;

namespace Elsa.API.Infrastructure.Projections;

/// <summary>
/// Проекция пользователя.
/// </summary>
class GetMeProjection : ElsaMinUser
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    public string Email { get; set; }
}
