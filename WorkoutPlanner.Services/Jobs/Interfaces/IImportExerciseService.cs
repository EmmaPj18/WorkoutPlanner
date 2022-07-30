namespace WorkoutPlanner.Services.Jobs.Interfaces;

public interface IImportExerciseService
{
    Task RunAsync(CancellationToken cancellationToken = default);
}
