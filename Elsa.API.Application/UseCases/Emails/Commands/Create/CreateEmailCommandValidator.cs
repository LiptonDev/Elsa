using FluentValidation;

namespace Elsa.API.Application.UseCases.Emails.Commands.Create;

/// <summary>
/// Валидация нового письма.
/// </summary>
public class CreateEmailCommandValidator : AbstractValidator<CreateEmailCommand>
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public CreateEmailCommandValidator()
    {
        RuleFor(x => x.Body).NotEmpty();
        RuleFor(x => x.Subject).NotEmpty();
        RuleFor(x => x.Recipients).ForEach(x => x.EmailAddress());
    }
}
