namespace WorkoutPlanner.Domain.Entities;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string ExplainVideoUrl { get; set; } = default!;
    public string? Description { get; set; }

    public ICollection<SetExercise> SetExercises { get; set; } = new HashSet<SetExercise>();
}
