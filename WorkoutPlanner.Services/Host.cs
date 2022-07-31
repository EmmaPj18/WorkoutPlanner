using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WorkoutPlanner.Common.Helpers;
using WorkoutPlanner.Services.Jobs.Exercises;
using WorkoutPlanner.Services.Jobs.Interfaces;

namespace WorkoutPlanner.Services;
public static class Host
{
    public static IServiceCollection AddWorkoutPlannerServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Host));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddAutoMapper(typeof(Host).Assembly);
        services.AddValidators(typeof(Host).Assembly);

        return services;
    }

    public static IServiceCollection AddWorkoutPlannerBatchJobServices(this IServiceCollection services)
    {
        services.AddScoped<IImportExerciseService, HttpImportExerciseService>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services, Assembly toScanAssembly)
    {
        var (found, types) = ServiceHelper.GetClassesImplementingAnInterface(toScanAssembly, typeof(IValidator<>));

        if (found)
        {
            foreach (var type in types)
            {
                services.Add(new ServiceDescriptor(type.GetInterfaces().First(), type, ServiceLifetime.Transient));
            }
        }

        return services;
    }
}
