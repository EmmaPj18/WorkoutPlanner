using FastEndpoints.Swagger;
using WorkoutPlanner.API;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration
    .AddSecretsJson();

// Services
builder.Services.AddWorkoutPlannerPersistenceServices(configuration);
builder.Services.AddWorkoutPlannerServices();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseMiddleware<DefaultExceptionMiddleware>();
app.UseFastEndpoints(x =>
{
    x.ErrorResponseBuilder = (failures, _) =>
    {
        return new FailureResponse
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Errors = failures.Select(y => y.ErrorMessage).ToList()
        };
    };
});

app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

await app.RunAsync();
