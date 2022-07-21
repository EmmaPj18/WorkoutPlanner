namespace WorkoutPlanner.Domain.Entities;

public class Routine
{
    public int Id { get; set; }
    public string ExplainVideoUrl { get; set; } = default!;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }

    public ICollection<UserRoutine> UserRoutines { get; set; } = new HashSet<UserRoutine>();
    public ICollection<RoutineSet> RoutineSets { get; set; } = new HashSet<RoutineSet>();
}
