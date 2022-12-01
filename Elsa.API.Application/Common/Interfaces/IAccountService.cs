﻿using Elsa.API.Application.UseCases.Account.Commands.Create;
using Elsa.API.Application.UseCases.Account.Queries;
using Elsa.Core.Enums;
using Elsa.Core.Models.Account;

namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Сервис аккаунтов.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Получить информацию о пользователях.
    /// </summary>
    /// <param name="userIds">Id'ы пользователей.</param>
    /// <returns></returns>
    Task<List<GetMeResponse>> GetUsersInfoAsync(string[] userIds);

    /// <summary>
    /// Регистрация пользователя.
    /// </summary>
    /// <param name="request">Данные для регистрации.</param>
    /// <returns>
    /// <para>bool - статус регистрации (true - успешно).</para>
    /// <para>string - первая ошибка регистрации.</para>
    /// </returns>
    Task<RegisterResponse> RegisterAsync(CreateUserCommand request);

    /// <summary>
    /// Попытка авторизации.
    /// </summary>
    /// <param name="request">Данные для авторизации.</param>
    /// <returns>
    /// <para>bool - статус авторизации (true - успешно).</para>
    /// <para>string - API ключ для авторизации.</para>
    /// </returns>
    Task<LoginResponse?> TryLoginAsync(GetAccessTokenCommand request);

    /// <summary>
    /// Проверяет, используется ли заданная почта у какого-то аккаунта.
    /// </summary>
    /// <param name="email">Почта.</param>
    /// <returns><see cref="bool">true</see> если почта используется.</returns>
    Task<bool> CheckEmailAsync(string email);

    /// <summary>
    /// Удалить токен(ы) авторизации.
    /// </summary>
    /// <param name="userId">Id пользователя, у которого будет удален(ы) токен(ы).</param>
    /// <param name="currentToken">Текущий токен (не передается при удалении всех токенов)</param>
    /// <param name="removeType">Тип удаления токена.</param>
    Task RemoveTokenAsync(string userId, string currentToken, RemoveTokenType removeType);
}
