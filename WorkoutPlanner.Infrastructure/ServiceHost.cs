using Microsoft.Extensions.DependencyInjection;
using WorkoutPlanner.Infrastructure.Exercise;
using WorkoutPlanner.Infrastructure.Exercise.Interfaces;

namespace WorkoutPlanner.Infrastructure;

public static class ServiceHost
{
    public static IServiceCollection AddInfrastructureWorkoutPlannerServices(this IServiceCollection services)
    {
        services.AddHttpClient<IExerciseClient, HttpExerciseClient>();

        return services;
    }
}
