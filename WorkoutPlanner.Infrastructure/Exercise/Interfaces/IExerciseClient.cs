using WorkoutPlanner.Infrastructure.Exercise.Contracts;

namespace WorkoutPlanner.Infrastructure.Exercise.Interfaces;

public interface IExerciseClient
{
    Task<ExerciseResponse> GetAll(CancellationToken cancellationToken = default);
}