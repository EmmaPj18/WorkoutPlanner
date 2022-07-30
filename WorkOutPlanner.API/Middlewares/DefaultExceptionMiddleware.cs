namespace WorkoutPlanner.API.Middlewares;

public class DefaultExceptionMiddleware
{
    private readonly RequestDelegate _request;

    public DefaultExceptionMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var response = new FailureResponse
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            response.Errors.Add(exception.Message);

            if (exception.InnerException != null)
                response.Errors.Add(exception.InnerException.Message);

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
