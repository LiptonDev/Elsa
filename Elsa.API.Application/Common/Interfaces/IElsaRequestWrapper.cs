using Elsa.API.Application.Common.Models;
using MediatR;

namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Оболочка ответа на запрос.
/// </summary>
public interface IElsaRequestWrapper<TResponse> : IRequest<ServiceResult<TResponse>>
{

}

/// <summary>
/// Оболочка обработчика запросов.
/// </summary>
public interface IElsaRequestHandlerWrapper<TRequest, TResponse> : IRequestHandler<TRequest, ServiceResult<TResponse>>
    where TRequest : IElsaRequestWrapper<TResponse>
{

}