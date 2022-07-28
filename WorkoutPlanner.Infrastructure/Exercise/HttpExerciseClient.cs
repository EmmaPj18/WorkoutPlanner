using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WorkoutPlanner.Common.Extensions;
using WorkoutPlanner.Infrastructure.Exercise.Contracts;
using WorkoutPlanner.Infrastructure.Exercise.Interfaces;
using WorkoutPlanner.Infrastructure.Exercise.Options;

namespace WorkoutPlanner.Infrastructure.Exercise;

public class HttpExerciseClient : IExerciseClient
{
    private readonly ILogger<HttpExerciseClient> _logger;
    private readonly HttpClient _httpClient;
    private readonly ExerciseClientOptions _options;
    private const string API_KEY_HEADER = "X-RapidAPI-Key";
    private const string API_HOST_HEADER = "X-RapidAPI-Host";
    private const string GET_ALL_ROUTE = "/exercises";

    public HttpExerciseClient(
        ILogger<HttpExerciseClient> logger,
        HttpClient httpClient,
        IOptions<ExerciseClientOptions> options)
    {
        _logger = logger;
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<ExerciseResponse> GetAll(CancellationToken cancellationToken = default)
    {
        var response = new ExerciseResponse();

        try
        {
            var request = CreateRequest();

            using var httpResponse = await _httpClient.SendAsync(request, cancellationToken);

            httpResponse.EnsureSuccessStatusCode();

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var recordList = jsonResponse.FromJson<List<ExerciseRecord>>();

            response.StatusCode = StatusCodes.Status200OK;
            response.Message = "Success";

            response.Records.AddRange(recordList!);
        }
        catch (HttpRequestException httpException)
        {
            _logger.LogError(httpException, "{className}.{methodName} HTTP request don't return a success code. Message: {message}",
                nameof(HttpExerciseClient),
                nameof(HttpExerciseClient.GetAll),
                httpException.Message);
            response.StatusCode = (int?)httpException.StatusCode ?? StatusCodes.Status500InternalServerError;
            response.Message = httpException.Message;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "{className}.{methodName} throws unhandle exception. Message: {message}",
                nameof(HttpExerciseClient),
                nameof(HttpExerciseClient.GetAll),
                exception.Message);
            response.StatusCode = StatusCodes.Status500InternalServerError;
            response.Message = exception.Message;
        }

        return response;
    }

    private HttpRequestMessage CreateRequest() => new()
    {
        Method = HttpMethod.Get,
        RequestUri = new Uri($"{_options.BaseUrl}/{GET_ALL_ROUTE}"),
        Headers =
        {
            { API_KEY_HEADER, _options.ApiKey },
            { API_HOST_HEADER, _options.ApiHost },
        }
    };
}
