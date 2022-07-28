using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutPlanner.Domain.Entities;
using WorkoutPlanner.Services.Interfaces;

namespace WorkoutPlanner.Persistence;

public abstract class WorkoutPlannerDbContextBase : DbContext
{
    public const string CONNECTION_STRING_NAME = "WorkoutPlanner_Db";
    public const string SCHEMA = "dbo";
    private readonly IEnumerable<IDataSeedService> _dataSeedServices;

    public WorkoutPlannerDbContextBase(DbContextOptions<WorkoutPlannerDbContextBase> options, IEnumerable<IDataSeedService> dataSeedServices)
    : base(options)
    {
        _dataSeedServices = dataSeedServices;
    }

    public WorkoutPlannerDbContextBase(DbContextOptions<ReadOnlyWorkoutPlannerDbContext> options, IEnumerable<IDataSeedService> dataSeedServices)
        : base(options)
    {
        _dataSeedServices = dataSeedServices;
    }

    public WorkoutPlannerDbContextBase(DbContextOptions<ReadWriteWorkoutPlannerDbContext> options, IEnumerable<IDataSeedService> dataSeedServices)
        : base(options)
    {
        _dataSeedServices = dataSeedServices;
    }

    public DbSet<Exercise> Exercises { get; set; } = default!;
    public DbSet<Routine> Routines { get; set; } = default!;
    public DbSet<RoutineSet> RoutineSets { get; set; } = default!;
    public DbSet<Set> Sets { get; set; } = default!;
    public DbSet<SetExercise> SetExercises { get; set; } = default!;
    public DbSet<UserRoutine> UserRoutines { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Lookup> Lookups { get; set; } = default!;
    public DbSet<BodyPart> BodyParts { get; set; } = default!;
    public DbSet<Equipment> Equipments { get; set; } = default!;
    public DbSet<TargetMuscle> TargetMuscles { get; set; } = default!;

    public DbSet<T> GetDbSet<T>() where T : class
    {
        return Set<T>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadOnlyWorkoutPlannerDbContext).Assembly);

        SetDateTimeKindUtc(modelBuilder);

        SeedDatabase(modelBuilder);
    }

    private void SeedDatabase(ModelBuilder modelBuilder)
    {
        foreach (var dataSeedService in _dataSeedServices)
        {
            dataSeedService.Seed(modelBuilder);
        }
    }

    private void SetDateTimeKindUtc(ModelBuilder modelBuilder)
    {
        //SEE: https://stackoverflow.com/questions/50727860/ef-core-2-1-hasconversion-on-all-properties-of-type-datetime

        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v,
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (!(property.ClrType == typeof(DateTime)
                    || property.ClrType == typeof(DateTime?)))
                {
                    continue;
                }

                if (!(property.Name.EndsWith("Utc", StringComparison.CurrentCultureIgnoreCase)
                    || property.Name.EndsWith("On", StringComparison.CurrentCultureIgnoreCase)))
                {
                    continue;
                }

                property.SetValueConverter(dateTimeConverter);
            }
        }
    }
}
