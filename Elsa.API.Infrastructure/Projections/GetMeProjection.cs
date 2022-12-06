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
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    public required string Email { get; set; }
}
