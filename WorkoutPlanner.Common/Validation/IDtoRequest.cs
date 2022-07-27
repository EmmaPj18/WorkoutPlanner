namespace WorkoutPlanner.Common.Validation;

public interface IDtoRequest<T>
{
    T Model { get; set; }
}
