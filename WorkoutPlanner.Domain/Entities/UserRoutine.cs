namespace WorkoutPlanner.Domain.Entities;

public class UserRoutine
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;

    public Guid RoutineId { get; set; }
    public virtual Routine Routine { get; set; } = default!;

    public DateTime WorkOutDate { get; set; } = DateTime.UtcNow;
}
