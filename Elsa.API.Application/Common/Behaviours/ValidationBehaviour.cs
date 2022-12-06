using Elsa.API.Application.Common.Models;
using Elsa.Core.Enums;
using Elsa.Core.Models.Errors;
using Elsa.Core.Models.Response;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace Elsa.API.Application.Common.Behaviours;

/// <summary>
/// Локализация для <see cref="ValidationBehaviour{TRequest, TResponse}"/>.
/// </summary>
public class ValidationBehaviourLocalization
{
    /// <summary>
    /// Локализация ошибки валидации.
    /// </summary>
    public const string InvalidRequest = nameof(InvalidRequest);
}

/// <summary>
/// MediatR pipline.
/// </summary>
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class, IServiceResult, new()
{
    private readonly IEnumerable<IValidator<TRequest>> validators;
    private readonly IStringLocalizer<ValidationBehaviourLocalization> localizer;
    private readonly IHttpContextAccessor httpContextAccessor;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators,
                               IStringLocalizer<ValidationBehaviourLocalization> localizer,
                               IHttpContextAccessor httpContextAccessor)
    {
        this.validators = validators;
        this.localizer = localizer;
        this.httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Обработчик.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var result = await Task.WhenAll(validators.Select(x => x.ValidateAsync(request, cancellationToken)));
            var fails = result.SelectMany(x => x.Errors).Where(x => x != null);
            if (fails.Any())
            {
                var errors = fails.GroupBy(x => x.PropertyName, x => x.ErrorMessage);
                var details = new ElsaError(localizer[ValidationBehaviourLocalization.InvalidRequest], ErrorCode.Validation, new ElsaValidationErrors(errors));
                var response = new TResponse { Error = details };
                httpContextAccessor.HttpContext.Response.StatusCode = 400; //bad request
                return response;
            }
        }

        var res = await next();

        httpContextAccessor.HttpContext.Response.StatusCode = (int)res.StatusCode;

        return res;
    }
}
