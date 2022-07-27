using System.ComponentModel;

namespace WorkoutPlanner.Domain.Enums;

public enum QuantityType
{
    [Description("Seconds per excersice rep")]
    Seconds = 1,
    [Description("Minutes per excersice rep")]
    Minutes,
    [Description("Repetitions over excercise")]
    Reps    
}
