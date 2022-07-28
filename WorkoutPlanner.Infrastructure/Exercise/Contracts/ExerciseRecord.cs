namespace WorkoutPlanner.Infrastructure.Exercise.Contracts;

public class ExerciseRecord
{
    public string Id { get; set; } = default!;
    public string BodyPart { get; set; } = default!;
    public string Equipment { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Target { get; set; } = default!;
}
