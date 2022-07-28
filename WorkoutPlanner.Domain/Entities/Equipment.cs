namespace WorkoutPlanner.Domain.Entities;

public class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public ICollection<Exercise> Exercises { get; set; } = new HashSet<Exercise>();
}
