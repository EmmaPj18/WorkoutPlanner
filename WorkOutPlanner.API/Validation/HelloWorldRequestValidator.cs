using FluentValidation;
using WorkoutPlanner.Domain.Contracts.Requests;

namespace WorkOutPlanner.API.Validation;

public class HelloWorldRequestValidator : AbstractValidator<HelloWorldRequest>
{
    public HelloWorldRequestValidator()
    {

    }
}
