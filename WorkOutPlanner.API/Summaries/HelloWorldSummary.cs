using FastEndpoints;
using WorkoutPlanner.Domain.Contracts.Responses;
using WorkOutPlanner.API.Endpoints;

namespace WorkOutPlanner.API.Summaries;

public class HelloWorldSummary : Summary<HelloWorldEndpoint>
{
    public HelloWorldSummary()
    {
        Summary = "Hello World!";
        Description = "Hello world";
        Response<HelloWorldResponse>(200, "Return name if specified");
    }
}
