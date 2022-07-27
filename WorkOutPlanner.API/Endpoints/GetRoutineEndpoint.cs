using WorkoutPlanner.Services.Routines;

namespace WorkOutPlanner.API.Endpoints;

[HttpGet("routine"), AllowAnonymous]
public class GetRoutineEndpoint : Endpoint<GetRoutineRequest, RoutineResponse>
{
    private readonly IMediator _mediator;

    public GetRoutineEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task HandleAsync(GetRoutineRequest req, CancellationToken ct)
    {
        var response = await _mediator.Send(new GetRoutineQuery { Model = req }, ct);

        await SendOkAsync(response, ct);
    }
}
