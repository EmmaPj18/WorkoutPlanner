using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using WorkoutPlanner.Persistence;
using WorkoutPlanner.Services;

namespace WorkoutPlanner.Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var context = builder.GetContext();

        builder.Services.AddWorkoutPlannerPersistenceServices(context.Configuration);
        builder.Services.AddWorkoutPlannerServices();
        builder.Services.AddWorkoutPlannerFunctionsServices();
    }
}
