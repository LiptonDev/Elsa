using Elsa.API.Application.Common.Exceptions;
using Elsa.API.Application.Common.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace Elsa.API.Application.Common.Behaviours;

/// <summary>
/// MediatR pipline.
/// </summary>
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;
    private readonly IStringLocalizer<ExceptionStrings> localizer;
    private readonly IHttpContextAccessor httpContextAccessor;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators,
                               IStringLocalizer<ExceptionStrings> localizer,
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
                throw new ElsaValidationException(localizer[ExceptionStrings.ValidationFailures], fails);
            }
        }

        var res = await next();

        if (res is IStatus status)
        {
            httpContextAccessor.HttpContext.Response.StatusCode = (int)status.StatusCode;
        }

        return res;
    }
}
