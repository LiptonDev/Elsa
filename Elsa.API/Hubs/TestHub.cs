using Elsa.API.Application;
using Elsa.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Elsa.API.Hubs;

[Authorize(AuthenticationSchemes = ElsaSchemeConsts.SchemeName)]
public class TestHub : Hub
{
    private readonly IHubConnectionsCollection collection;

    public TestHub(IHubConnectionsCollection collection)
    {
        this.collection = collection;
    }

    public string Ping()
    {
        return "Pong at " + DateTime.Now;
    }

    public override Task OnConnectedAsync()
    {
        var token = Context.GetHttpContext().Request.Headers[ElsaSchemeConsts.SchemeBearer];
        return collection.AddAsync(token, Context);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var token = Context.GetHttpContext().Request.Headers[ElsaSchemeConsts.SchemeBearer];

        return collection.DisconnectAsync(token);
    }
}
