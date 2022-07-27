namespace WorkoutPlanner.Services.Interfaces;

public interface IReadOnlyWorkoutPlannerDbContext : IDisposable
{
    DbSet<Exercise> Exercises { get; set; }
    DbSet<Routine> Routines { get; set; }
    DbSet<RoutineSet> RoutineSets { get; set; }
    DbSet<Set> Sets { get; set; }
    DbSet<SetExercise> SetExercises { get; set; }
    DbSet<UserRoutine> UserRoutines { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Lookup> Lookups { get; set; }
    DbSet<T> GetDbSet<T>() where T : class;
}