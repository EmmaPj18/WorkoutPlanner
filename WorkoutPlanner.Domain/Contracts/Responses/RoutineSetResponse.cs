using WorkoutPlanner.Domain.Enums;

namespace WorkoutPlanner.Domain.Contracts.Responses;

public class RoutineSetResponse
{
    public int Order { get; set; }
    public SetType SetType { get; set; }
    public string Name { get; set; } = default!;
    public string ExplainVideoUrl { get; set; } = default!;
    public string? Description { get; set; }
    public int RoundsNumber { get; set; }

    public List<SetExerciseResponse> Exercises { get; set; } = new();
}
