using Elsa.Core.Models.Account.Request;
using FluentValidation;

namespace Elsa.API.Application.UseCases.Account.Queries.GetPasswordResetToken;

/// <summary>
/// Валидация Email.
/// </summary>
public class GetResetPasswordTokenCommandValidator : AbstractValidator<GetResetPasswordTokenCommand>
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public GetResetPasswordTokenCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
