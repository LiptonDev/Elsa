using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Elsa.API.Infrastructure.Authentication.Contexts;

public class ForbiddenContext : ResultContext<ElsaSchemeOptions>
{
    public ForbiddenContext(HttpContext context, AuthenticationScheme scheme, ElsaSchemeOptions options) : base(context, scheme, options)
    {
    }
}
