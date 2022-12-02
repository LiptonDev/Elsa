using Elsa.API.Application.Common.Exceptions;
using Elsa.Core.Enums;
using Elsa.Core.Models.Errors;
using Elsa.Core.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;

namespace Elsa.API.Filters;

/// <summary>
/// Фильтр ошибок API.
/// </summary>
class ApiExceptionsFilterAttribute : ExceptionFilterAttribute
{
    /// <summary>
    /// Обработчики ошибок.
    /// </summary>
    private readonly Dictionary<Type, Action<ExceptionContext>> handlers;

    /// <summary>
    /// Локализация ошибок.
    /// </summary>
    private readonly IStringLocalizer<ElsaExceptions> localizer;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ApiExceptionsFilterAttribute(IStringLocalizer<ElsaExceptions> localizer)
    {
        handlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ElsaValidationException), HandleValidationException },
            { typeof(ElsaNotFoundExceptions), HandleNotFoundException },
            { typeof(ElsaApiException), HandleApiException }
        };

        this.localizer = localizer;
    }

    /// <summary>
    /// Вызывается при ошибке API.
    /// </summary>
    /// <param name="context"></param>
    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    /// <summary>
    /// Обработка ошибок.
    /// </summary>
    /// <param name="context"></param>
    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (handlers.ContainsKey(type))
        {
            handlers[type].Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }

    /// <summary>
    /// Обработка необработанного исключения.
    /// </summary>
    private void HandleUnknownException(ExceptionContext context)
    {
        var details = new ElsaResult(new ElsaError(localizer[ElsaExceptions.UnhandledException], ErrorCode.Unhandled));

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }

    /// <summary>
    /// Обработка ошибки API.
    /// </summary>
    private void HandleApiException(ExceptionContext context)
    {
        if (context.Exception is ElsaApiException ex)
        {
            var details = new ElsaResult(new ElsaError(ex.Message, ex.ErrorCode));
            context.Result = new ObjectResult(details)
            {
                StatusCode = (int)ex.StatusCode
            };
        }

        context.ExceptionHandled = true;
    }

    /// <summary>
    /// Обработка ошибки поиска.
    /// </summary>
    private void HandleNotFoundException(ExceptionContext context)
    {
        if (context.Exception is ElsaNotFoundExceptions ex)
        {
            var msg = string.Format(localizer[ElsaExceptions.NotFoundException], ex.Key);
            var details = new ElsaResult(new ElsaError(msg, ErrorCode.ValueNotFound));
            context.Result = new NotFoundObjectResult(details);
        }

        context.ExceptionHandled = true;
    }

    /// <summary>
    /// Обработка ошибки валидации.
    /// </summary>
    private void HandleValidationException(ExceptionContext context)
    {
        if (context.Exception is ElsaValidationException ex)
        {
            var details = new ElsaResult(new ElsaError(localizer[ElsaExceptions.ValidationException], ErrorCode.Validation, new ElsaValidationErrors(ex.Errors)));
            context.Result = new BadRequestObjectResult(details);
        }

        context.ExceptionHandled = true;
    }
}
