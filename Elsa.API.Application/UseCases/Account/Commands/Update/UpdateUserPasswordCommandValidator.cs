using Elsa.API.Domain.Settings;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Elsa.API.Application.UseCases.Account.Commands.Update;

/// <summary>
/// Валидация данных для смены пароля.
/// </summary>
public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public UpdateUserPasswordCommandValidator(
        IStringLocalizer<IdentityStrings> localizer,
        IOptions<RegexSettings> options)
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.ResetToken).NotEmpty().When(x => !x.IsAdminAction);
        RuleFor(x => x.NewPassword).NotEmpty().Matches(options.Value.UserPasswordPattern);
        RuleFor(x => x.ConfirmPassword).Equal(x => x.NewPassword).WithMessage(localizer[IdentityStrings.ConfirmPassword]);
    }
}
