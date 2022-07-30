using FastEndpoints;
using WorkoutPlanner.Domain.Contracts.Responses;
using WorkoutPlanner.API.Endpoints;

namespace WorkoutPlanner.API.Summaries;

public class HelloWorldSummary : Summary<HelloWorldEndpoint>
{
    public HelloWorldSummary()
    {
        Summary = "Hello World!";
        Description = "Hello world";
        Response<HelloWorldResponse>(200, "Return name if specified");
    }
}
