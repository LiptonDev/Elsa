using AutoMapper;
using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.API.Domain.Entities;
using Elsa.Core.Models.Emails.Request;
using Elsa.Core.Models.Emails.Response;

namespace Elsa.API.Application.UseCases.Emails.Commands.Create;

/// <summary>
/// Команда создания письма.
/// </summary>
public class CreateEmailCommand : AddEmailToQueueRequest, IElsaRequestWrapper<AddEmailToQueueResponse>
{
}

/// <summary>
/// Обработчик добавления письма в очередь.
/// </summary>
public class CreateEmailCommandHandler : IElsaRequestHandlerWrapper<CreateEmailCommand, AddEmailToQueueResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork<int> unitOfWork;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public CreateEmailCommandHandler(IMapper mapper, IUnitOfWork<int> unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Обработчик.
    /// </summary>
    /// <param name="request">Письмо.</param>
    public async Task<ServiceResult<AddEmailToQueueResponse>> Handle(CreateEmailCommand request, CancellationToken cancellationToken)
    {
        var map = mapper.Map<EmailEntity>(request);
        await unitOfWork.Repository<EmailEntity>().AddAsync(map, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        return new ServiceResult<AddEmailToQueueResponse>(new AddEmailToQueueResponse { Succeeded = true });
    }
}