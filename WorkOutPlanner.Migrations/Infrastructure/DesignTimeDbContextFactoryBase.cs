using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WorkoutPlanner.Persistence;

namespace WorkoutPlanner.Migrations.Infrastructure;

public abstract class DesignTimeDbContextFactoryBase<TContext> :
    IDesignTimeDbContextFactory<TContext> where TContext : DbContext
{
    private const string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";

    public TContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        return Create(basePath, Environment.GetEnvironmentVariable(ASPNETCORE_ENVIRONMENT)!);
    }

    protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

    private TContext Create(string basePath, string environment)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString(WorkoutPlannerDbContextBase.CONNECTION_STRING_NAME);

        return Create(connectionString);
    }

    private TContext Create(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("Connection string is null or empty", nameof(connectionString));
        }

        var assemblyName = typeof(DesignTimeDbContextFactoryBase<>)
            .Assembly.GetName().Name;

        var optionBuilder = new DbContextOptionsBuilder<TContext>();

        optionBuilder.UseSqlServer(connectionString!, builder =>
        {
            builder.MigrationsAssembly(assemblyName)
                .MigrationsHistoryTable("__ef_migrations_history", WorkoutPlannerDbContextBase.SCHEMA)
                .CommandTimeout(1800);

            builder.EnableRetryOnFailure(
                maxRetryCount: 2,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        });

        return CreateNewInstance(optionBuilder.Options);
    }
}
