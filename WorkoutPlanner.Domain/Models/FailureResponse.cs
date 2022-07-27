namespace WorkoutPlanner.Domain.Models;

public class FailureResponse
{
    public int StatusCode { get; set; }
    public List<string> Errors { get; set; } = new();
};