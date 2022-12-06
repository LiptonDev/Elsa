using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Elsa.API.Controllers;

/// <summary>
/// Базовый контроллер.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    private IMediator mediator;
    /// <summary>
    /// Mideator.
    /// </summary>
    protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
}
