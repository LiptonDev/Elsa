﻿namespace Elsa.API.Infrastructure.Projections.MinUser;

/// <summary>
/// Минимальная модель ролей пользователей.
/// </summary>
class ElsaMinUserRole
{
    /// <summary>
    /// Роль.
    /// </summary>
    public ElsaMinRole Role { get; set; }
}
