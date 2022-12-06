using Elsa.Core.Enums;

namespace Elsa.Core.Models.Account.Request;

/// <summary>
/// Удалить токены авторизации пользователя.
/// </summary>
public class DeleteUserTokensRequest
{
    /// <summary>
    /// Id пользователя, у которого будет удален токен.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Токен, который нужно удалить (используется для <see cref="RemoveTokenType.JustCurrent"/> и <see cref="RemoveTokenType.AllExceptCurrent"/>).
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Тип удаления токенов.
    /// </summary>
    public required RemoveTokenType RemoveType { get; set; }
}
