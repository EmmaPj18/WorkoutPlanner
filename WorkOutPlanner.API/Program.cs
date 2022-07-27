using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Services
builder.Services.AddWorkoutPlannerPersistenceServices(configuration);
builder.Services.AddWorkoutPlannerServices();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseFastEndpoints(x =>
{
    x.ErrorResponseBuilder = (failures, _) =>
    {
        return new FailureResponse
        {
            Errors = failures.Select(y => y.ErrorMessage).ToList()
        };
    };
});

app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

await app.RunAsync();
