namespace WorkoutPlanner.Domain.Models;

public class ValidationFailureResponse
{
    public List<string> Errors { get; set; } = default!;
};