using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Services.Interfaces;

namespace WorkoutPlanner.Persistence;

public class ReadOnlyWorkoutPlannerDbContext : WorkoutPlannerDbContextBase, IReadOnlyWorkoutPlannerDbContext
{
    public ReadOnlyWorkoutPlannerDbContext(DbContextOptions<ReadOnlyWorkoutPlannerDbContext> options, IEnumerable<IDataSeedService> dataSeedServices)
        : base(options, dataSeedServices)
    {
    }
}
