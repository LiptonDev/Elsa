﻿using Elsa.Core.Models.Response;
using MediatR;

namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Оболочка ответа на запрос.
/// </summary>
public interface IElsaRequestWrapper<TResponse> : IRequest<ElsaResult<TResponse>>
{

}

/// <summary>
/// Оболочка обработчика запросов.
/// </summary>
public interface IElsaRequestHandlerWrapper<TRequest, TResponse> : IRequestHandler<TRequest, ElsaResult<TResponse>>
    where TRequest : IElsaRequestWrapper<TResponse>
{

}

/// <summary>
/// Оболочка обработчика запросов.
/// </summary>
public interface IElsaRequestHandlerWrapper<TRequest> : IRequestHandler<TRequest, ElsaResult>
    where TRequest : IElsaRequestWrapper<ElsaResult>
{

}