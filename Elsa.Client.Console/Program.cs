using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

var token = "Elsa.hHoUJrYeJxAKPTHILMY9S64Uea4GOmJUhcSvcxXsIoZmd31hQ6fi85VJEDz3miBTpGC9moLEOcohusCU1gzLfZqf8n9JlHbdQ8gfWXzxwr8iDiEt6ok4y2p8o7A2guDH";

var client = new HubConnectionBuilder().WithUrl("https://localhost:5001/test", x =>
{
    x.Headers.Add("Elsa-Token", token);
}).WithAutomaticReconnect().Build();
client.Closed += Client_Closed;

Task Client_Closed(Exception? arg)
{
    Console.WriteLine($"Closed connection: {arg?.Message}");
    return Task.CompletedTask;
}

await client.StartAsync();

while (true)
{
    try
    {
        Console.WriteLine(await client.InvokeAsync<string>("Ping"));
    }
    catch
    {
        break;
    }
    await Task.Delay(1000);
}

Console.ReadLine();