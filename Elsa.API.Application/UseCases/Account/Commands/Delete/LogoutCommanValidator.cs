using Elsa.Core.Enums;
using FluentValidation;

namespace Elsa.API.Application.UseCases.Account.Commands.Delete;

/// <summary>
/// Валидация данных для выхода из системы.
/// </summary>
public class LogoutCommanValidator : AbstractValidator<LogoutCommand>
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public LogoutCommanValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().Must(x => Guid.TryParse(x, out _)).WithErrorCode(CustomErrorCodes.ElsaGuidValidator);
        RuleFor(x => x.Token).NotEmpty().When(x => x.RemoveType == RemoveTokenType.JustCurrent || x.RemoveType == RemoveTokenType.AllExceptCurrent);
        RuleFor(x => x.RemoveType).IsInEnum();
    }
}
