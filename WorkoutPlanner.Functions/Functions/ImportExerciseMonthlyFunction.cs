namespace WorkoutPlanner.Functions.Functions;

public class ImportExerciseMonthlyFunction
{
    private const string SERVICE_NAME = "ImportExerciseMonthly";

    private readonly ILogger<ImportExerciseMonthlyFunction> _logger;
    private readonly IImportExerciseService _exerciseService;

    public ImportExerciseMonthlyFunction(ILogger<ImportExerciseMonthlyFunction> logger,
        IImportExerciseService exerciseService)
    {
        _logger = logger;
        _exerciseService = exerciseService;
    }

    [FunctionName(SERVICE_NAME)]
    public async Task RunAsync([TimerTrigger("0 4 31 * *")] TimerInfo _, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"{SERVICE_NAME} Timer trigger function started at: {DateTime.UtcNow}");

            await _exerciseService.RunAsync(cancellationToken);

            _logger.LogInformation($"{SERVICE_NAME} Timer trigger function finished at: {DateTime.UtcNow}");
        }
        catch (Exception ex)
        {
            var message = ex.InnerException is null ? ex.Message : ex.InnerException.Message;

            _logger.LogError(ex, "{service} Timer trigger function throws error. Message: {message}",
                SERVICE_NAME,
                message);
        }
    }
}
