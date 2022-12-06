using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Domain.Settings;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Elsa.API.Application.UseCases.Account.Commands.Create;

/// <summary>
/// Валидация данных для регистрации.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IAccountService accountService;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public CreateUserCommandValidator(
        IStringLocalizer<IdentityStrings> localizer,
        IOptions<RegexSettings> options,
        IAccountService accountService)
    {
        this.accountService = accountService;
        RuleFor(x => x.Email).EmailAddress()
                             .MustAsync(CheckEmailInDb).WithMessage(localizer[IdentityStrings.EmailIsInUse]);

        RuleFor(x => x.Password).NotEmpty()
                                .Matches(options.Value.UserPasswordPattern);

        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage(localizer[IdentityStrings.ConfirmPassword]);

        RuleFor(x => x.FirstName).NotEmpty()
                                 .Matches(options.Value.UserFirstNamePattern);

        RuleFor(x => x.LastName).NotEmpty()
                                .Matches(options.Value.UserLastNamePattern);
    }

    /// <summary>
    /// Проверяет, НЕ используется ли указанная почта у какого-либо пользователя.
    /// </summary>
    /// <param name="email">Почта.</param>
    /// <returns><see cref="bool">true</see> если почта НЕ используется</returns>
    async Task<bool> CheckEmailInDb(string email, CancellationToken token)
    {
        return !await accountService.CheckEmailAsync(email, token);
    }
}
