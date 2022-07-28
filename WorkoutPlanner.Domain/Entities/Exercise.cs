namespace WorkoutPlanner.Domain.Entities;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? ExplainVideoUrl { get; set; }
    public string? Description { get; set; }
    public int EquipmentId { get; set; }
    public int BodyPartId { get; set; }
    public int TargetMuscleId { get; set; }

    public Equipment? Equipment { get; set; }
    public BodyPart? BodyPart { get; set; }
    public TargetMuscle? TargetMuscle { get; set; }

    public ICollection<SetExercise> SetExercises { get; set; } = new HashSet<SetExercise>();
}
