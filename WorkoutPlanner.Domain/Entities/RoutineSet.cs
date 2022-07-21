namespace WorkoutPlanner.Domain.Entities;

public class RoutineSet
{
    public int RoutineId { get; set; }
    public virtual Routine Routine { get; set; } = default!;
    public int SetId { get; set; }
    public virtual Set Set { get; set; } = default!;
    public int Order { get; set; }
}
