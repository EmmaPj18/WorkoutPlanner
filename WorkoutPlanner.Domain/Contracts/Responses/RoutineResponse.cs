namespace WorkoutPlanner.Domain.Contracts.Responses;

public class RoutineResponse
{
    public Guid Id { get; set; }
    public string ExplainVideoUrl { get; set; } = default!;
    public string? Description { get; set; }

    public List<RoutineSetResponse> Sets { get; set; } = new();
}
