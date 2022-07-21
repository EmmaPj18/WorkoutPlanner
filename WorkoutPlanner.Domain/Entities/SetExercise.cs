namespace WorkoutPlanner.Domain.Entities;

public class SetExercise
{
    public int SetId { get; set; }
    public virtual Set Set { get; set; } = default!;

    public int ExerciseId { get; set; }
    public virtual Exercise Exercise { get; set; } = default!;
}
