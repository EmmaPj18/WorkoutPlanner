namespace WorkoutPlanner.Domain.Entities;

public class Lookup
{
    public int Id { get; set; }
    public string? Category { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
}
