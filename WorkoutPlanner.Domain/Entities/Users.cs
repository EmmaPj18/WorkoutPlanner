namespace WorkoutPlanner.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public DateOnly BirthDay { get; set; } = default!;

    public ICollection<UserRoutine> UserRoutines { get; set; } = new HashSet<UserRoutine>();
}
