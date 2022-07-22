using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkoutPlanner.Services.Interfaces;

namespace WorkoutPlanner.Persistence;

public static class Host
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataSeeders();
        services.AddReadWriteWorkOutPlannerDbContext(configuration);
        services.AddReadOnlyWorkOutPlannerDbContext(configuration);

        return services;
    }

    private static IServiceCollection AddDataSeeders(this IServiceCollection services)
    {
        var interfaceType = typeof(IDataSeedService);
        var types = typeof(Host).Assembly
            .GetTypes()
            .Where(p => interfaceType.IsAssignableFrom(p));

        foreach (var implementationType in types)
        {
            services.AddTransient(interfaceType, implementationType);
        }        

        return services;
    }

    private static IServiceCollection AddReadWriteWorkOutPlannerDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddDbContext<IReadWriteWorkoutPlannerDbContext, ReadWriteWorkoutPlannerDbContext>(options =>
        {
            options.ApplyBuilderOptions(isReadOnly: false,
                connectionString: configuration.GetConnectionString(ReadWriteWorkoutPlannerDbContext.ConnectionString));
        });
    }

    private static IServiceCollection AddReadOnlyWorkOutPlannerDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddDbContext<IReadOnlyWorkoutPlannerDbContext, ReadOnlyWorkoutPlannerDbContext>(options =>
        {
            options.ApplyBuilderOptions(isReadOnly: true,
                connectionString: configuration.GetConnectionString(ReadWriteWorkoutPlannerDbContext.ConnectionString));
        });
    }

    private static void ApplyBuilderOptions(this DbContextOptionsBuilder optionsBuilder,
        bool isReadOnly,
        string connectionString)
    {
        if (isReadOnly)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        optionsBuilder.UseSqlServer(connectionString);
    }
}
