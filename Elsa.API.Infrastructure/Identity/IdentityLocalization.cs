using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Elsa.API.Infrastructure.Identity;

/// <summary>
/// Локализация identity для русского языка.
/// </summary>
class IdentityLocalization : IdentityErrorDescriber
{
    private readonly IStringLocalizer<IdentityLocalization> localizer;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public IdentityLocalization(IStringLocalizer<IdentityLocalization> localizer)
    {
        this.localizer = localizer;
    }

    /// <summary>
    /// Не удалось активировать код восстановления.
    /// </summary>
    /// <returns></returns>
    public override IdentityError RecoveryCodeRedemptionFailed() =>
        new() { Code = nameof(RecoveryCodeRedemptionFailed), Description = localizer[nameof(RecoveryCodeRedemptionFailed)] };

    /// <summary>
    /// Пароль должен иметь уникальные символы.
    /// </summary>
    /// <param name="uniqueChars"></param>
    /// <returns></returns>
    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) =>
        new() { Code = nameof(PasswordRequiresUniqueChars), Description = string.Format(localizer[nameof(PasswordRequiresUniqueChars)], uniqueChars) };

    /// <summary>
    /// Неизвестная ошибка.
    /// </summary>
    /// <returns></returns>
    public override IdentityError DefaultError() =>
        new() { Code = nameof(DefaultError), Description = localizer[nameof(DefaultError)] };

    /// <summary>
    /// Объект был изменен.
    /// </summary>
    /// <returns></returns>
    public override IdentityError ConcurrencyFailure() =>
        new() { Code = nameof(ConcurrencyFailure), Description = localizer[nameof(ConcurrencyFailure)] };

    /// <summary>
    /// Пароль не совпадает.
    /// </summary>
    /// <returns></returns>
    public override IdentityError PasswordMismatch() =>
        new() { Code = nameof(PasswordMismatch), Description = localizer[nameof(PasswordMismatch)] };

    /// <summary>
    /// Неправильный токен.
    /// </summary>
    /// <returns></returns>
    public override IdentityError InvalidToken() =>
        new() { Code = nameof(InvalidToken), Description = localizer[nameof(InvalidToken)] };

    /// <summary>
    /// Логин уже используется.
    /// </summary>
    /// <returns></returns>
    public override IdentityError LoginAlreadyAssociated() =>
        new() { Code = nameof(LoginAlreadyAssociated), Description = localizer[nameof(LoginAlreadyAssociated)] };

    /// <summary>
    /// Неправильное имя пользователя.
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public override IdentityError InvalidUserName(string? userName) =>
        new() { Code = nameof(InvalidUserName), Description = string.Format(localizer[nameof(InvalidUserName)], userName) };

    /// <summary>
    /// Неправильная почта.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public override IdentityError InvalidEmail(string? email) =>
        new() { Code = nameof(InvalidEmail), Description = string.Format(localizer[nameof(InvalidEmail)], email) };

    /// <summary>
    /// Дублирование имени пользователя.
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public override IdentityError DuplicateUserName(string userName) =>
        new() { Code = nameof(DuplicateUserName), Description = string.Format(localizer[nameof(DuplicateUserName)], userName) };

    /// <summary>
    /// Дублирование почты.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public override IdentityError DuplicateEmail(string email) =>
        new() { Code = nameof(DuplicateEmail), Description = string.Format(localizer[nameof(DuplicateEmail)], email) };

    /// <summary>
    /// Неправильное имя роли.
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public override IdentityError InvalidRoleName(string? role) =>
        new() { Code = nameof(InvalidRoleName), Description = string.Format(localizer[nameof(InvalidUserName)], role) };

    /// <summary>
    /// Роль с таким именем уже существует.
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public override IdentityError DuplicateRoleName(string role) =>
        new() { Code = nameof(DuplicateRoleName), Description = string.Format(localizer[nameof(DuplicateRoleName)], role) };

    /// <summary>
    /// У пользователя уже установлен пароль.
    /// </summary>
    /// <returns></returns>
    public override IdentityError UserAlreadyHasPassword() =>
        new() { Code = nameof(UserAlreadyHasPassword), Description = localizer[nameof(UserAlreadyHasPassword)] };

    /// <summary>
    /// Блокировка не включена.
    /// </summary>
    /// <returns></returns>
    public override IdentityError UserLockoutNotEnabled() =>
        new() { Code = nameof(UserLockoutNotEnabled), Description = localizer[nameof(UserLockoutNotEnabled)] };

    /// <summary>
    /// Пользователь уже имеет роль.
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public override IdentityError UserAlreadyInRole(string role) =>
        new() { Code = nameof(UserAlreadyInRole), Description = string.Format(localizer[nameof(UserAlreadyInRole)], role) };

    /// <summary>
    /// Пользователь не находится в роли.
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public override IdentityError UserNotInRole(string role) =>
        new() { Code = nameof(UserNotInRole), Description = string.Format(localizer[nameof(UserNotInRole)], role) };

    /// <summary>
    /// Пароль короткий.
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public override IdentityError PasswordTooShort(int length) =>
        new() { Code = nameof(PasswordTooShort), Description = string.Format(localizer[nameof(PasswordTooShort)], length) };

    /// <summary>
    /// Пароль должен иметь не алфавитные символы.
    /// </summary>
    /// <returns></returns>
    public override IdentityError PasswordRequiresNonAlphanumeric() =>
        new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = localizer[nameof(PasswordRequiresNonAlphanumeric)] };

    /// <summary>
    /// Пароль должен иметь цифры.
    /// </summary>
    /// <returns></returns>
    public override IdentityError PasswordRequiresDigit() =>
        new() { Code = nameof(PasswordRequiresDigit), Description = localizer[nameof(PasswordRequiresDigit)] };

    /// <summary>
    /// Пароль должен иметь буквы в нижнем регистре.
    /// </summary>
    /// <returns></returns>
    public override IdentityError PasswordRequiresLower() =>
        new() { Code = nameof(PasswordRequiresLower), Description = localizer[nameof(PasswordRequiresLower)] };

    /// <summary>
    /// Пароль должен иметь буквы в верхнем регистре.
    /// </summary>
    /// <returns></returns>
    public override IdentityError PasswordRequiresUpper() =>
        new() { Code = nameof(PasswordRequiresUpper), Description = localizer[nameof(PasswordRequiresUpper)] };
}
