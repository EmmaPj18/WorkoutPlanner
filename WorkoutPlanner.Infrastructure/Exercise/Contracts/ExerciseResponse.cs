using Microsoft.AspNetCore.Http;

namespace WorkoutPlanner.Infrastructure.Exercise.Contracts;

public partial class ExerciseResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public List<ExerciseRecord> Records { get; set; } = new();
}

public partial class ExerciseResponse
{
    public bool IsSuccess => StatusCode == StatusCodes.Status200OK;
}
