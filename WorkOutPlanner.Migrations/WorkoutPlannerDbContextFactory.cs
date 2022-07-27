using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Migrations.Infrastructure;
using WorkoutPlanner.Persistence;
using WorkoutPlanner.Services.Interfaces;

namespace WorkoutPlanner.Migrations;

internal class WorkoutPlannerDbContextFactory : DesignTimeDbContextFactoryBase<ReadWriteWorkoutPlannerDbContext>
{
    protected override ReadWriteWorkoutPlannerDbContext CreateNewInstance(
        DbContextOptions<ReadWriteWorkoutPlannerDbContext> options)
    {
        List<IDataSeedService> dataServices = GetDataSeedServices();

        var context = new ReadWriteWorkoutPlannerDbContext(options, dataServices);

        context.Database.SetCommandTimeout(1800);

        return context;
    }

    private static List<IDataSeedService> GetDataSeedServices()
    {
        var interfaceType = typeof(IDataSeedService);
        var types = typeof(Host).Assembly
            .GetTypes()
            .Where(p => interfaceType.IsAssignableFrom(p));

        var dataServices = new List<IDataSeedService>();

        foreach (var type in types)
        {
            dataServices.Add((IDataSeedService)Activator.CreateInstance(type)!);
        }

        return dataServices;
    }
}
