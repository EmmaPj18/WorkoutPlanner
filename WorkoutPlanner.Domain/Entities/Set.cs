using WorkoutPlanner.Domain.Enums;

namespace WorkoutPlanner.Domain.Entities;

public class Set
{
    public int Id { get; set; }
    public SetType Type { get; set; }
    public string Name { get; set; } = default!;
    public string ExplainVideoUrl { get; set; } = default!;
    public string? Description { get; set; }
    public int RoundsNumber { get; set; }

    public ICollection<RoutineSet> RoutineSets { get; set; } = new HashSet<RoutineSet>();
    public ICollection<SetExercise> SetExercises { get; set; } = new HashSet<SetExercise>();
}
