﻿namespace Elsa.Core.Models.Account.Request;

/// <summary>
/// Данные для авторизации.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Почта.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; }
}