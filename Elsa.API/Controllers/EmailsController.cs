using AutoMapper;
using Elsa.API.Application.Common.Models;
using Elsa.API.Application.UseCases.Emails.Commands.Create;
using Elsa.Core.Enums;
using Elsa.Core.Models.Emails.Request;
using Elsa.Core.Models.Emails.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elsa.API.Controllers;

/// <summary>
/// Контроллер рассылки писем.
/// </summary>
[Authorize(Roles = nameof(Roles.Admin))]
public class EmailsController : BaseController
{
    /// <summary>
    /// Добавить письмо в очередь.
    /// </summary>
    /// <param name="request">Письмо.</param>
    /// <returns></returns>
    [HttpPost(nameof(Add))]
    public Task<ServiceResult<AddEmailToQueueResponse>> Add([FromServices] IMapper mapper, [FromBody] AddEmailToQueueRequest request)
    {
        var map = mapper.Map<CreateEmailCommand>(request);
        return Mediator.Send(map);
    }
}
