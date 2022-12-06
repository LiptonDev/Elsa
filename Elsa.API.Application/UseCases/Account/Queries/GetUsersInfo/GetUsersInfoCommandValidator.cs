using FluentValidation;

namespace Elsa.API.Application.UseCases.Account.Queries.GetUsersInfo;

/// <summary>
/// Валидация данных для получения 
/// </summary>
public class GetUsersInfoCommandValidator : AbstractValidator<GetUsersInfoCommand>
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public GetUsersInfoCommandValidator()
    {
        RuleFor(x => x.UserIds).NotEmpty().ForEach(x => x.Must(p => Guid.TryParse(p, out _)).WithErrorCode(CustomErrorCodes.ElsaGuidValidator));
    }
}
