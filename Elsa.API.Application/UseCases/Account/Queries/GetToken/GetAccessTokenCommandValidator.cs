using Elsa.API.Domain.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Elsa.API.Application.UseCases.Account.Queries.GetToken;

/// <summary>
/// Валидация данных для авторизации.
/// </summary>
public class GetAccessTokenCommandValidator : AbstractValidator<GetAccessTokenCommand>
{
    /// <summary>
    /// Констуктор.
    /// </summary>
    public GetAccessTokenCommandValidator(IOptions<RegexSettings> options)
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty().Matches(options.Value.UserPasswordPattern);
    }
}
