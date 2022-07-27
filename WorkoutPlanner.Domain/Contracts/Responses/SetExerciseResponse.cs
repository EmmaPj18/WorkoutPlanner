using WorkoutPlanner.Domain.Enums;

namespace WorkoutPlanner.Domain.Contracts.Responses;

public class SetExerciseResponse
{
    public int Order { get; set; }
    public double Quantity { get; set; }
    public QuantityType QuantityType { get; set; }
    public string Name { get; set; } = default!;
    public string ExplainVideoUrl { get; set; } = default!;
    public string? Description { get; set; }
}
