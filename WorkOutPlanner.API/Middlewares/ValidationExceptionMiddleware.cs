using FluentValidation;

namespace WorkoutPlanner.API.Middlewares;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _request;

    public ValidationExceptionMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var messages = exception.Errors.Select(x => x.ErrorMessage).ToList();
            var validationFailureResponse = new FailureResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Errors = messages
            };
            await context.Response.WriteAsJsonAsync(validationFailureResponse);
        }
    }
}
