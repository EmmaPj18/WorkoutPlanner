namespace WorkoutPlanner.Services.Interfaces;

public interface IReadWriteWorkoutPlannerDbContext : IReadOnlyWorkoutPlannerDbContext
{
    int SaveChanges(bool acceptAllChangesOnSuccess);
    int SaveChanges();
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
