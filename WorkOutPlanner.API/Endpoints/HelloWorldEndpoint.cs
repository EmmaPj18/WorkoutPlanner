using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using WorkoutPlanner.Domain.Contracts.Requests;
using WorkoutPlanner.Domain.Contracts.Responses;

namespace WorkOutPlanner.API.Endpoints;

[HttpGet("helloWorld"), AllowAnonymous]
public class HelloWorldEndpoint : Endpoint<HelloWorldRequest, HelloWorldResponse>
{
    public override async Task HandleAsync(HelloWorldRequest req, CancellationToken ct)
    {
        var response = new HelloWorldResponse
        {
            Message = !string.IsNullOrWhiteSpace(req.Name) ?
                $"Hello {req.Name}"
                : "Hello World!"
        };

        await SendOkAsync(response, ct);
    }
}
