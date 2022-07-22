using WorkoutPlanner.Domain.Enums;

namespace WorkoutPlanner.Domain.Entities;

public class SetExercise
{
    public int SetId { get; set; }
    public virtual Set Set { get; set; } = default!;

    public int ExerciseId { get; set; }
    public virtual Exercise Exercise { get; set; } = default!;

    public int Order { get; set; }
    public double Quantity { get; set; }
    public QuantityType QuantityType { get; set; }
}
