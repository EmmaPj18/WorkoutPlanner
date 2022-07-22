using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutPlanner.Domain.Entities;

namespace WorkoutPlanner.Persistence.Configurations;

public class RoutineSetConfiguration : IEntityTypeConfiguration<RoutineSet>
{
    public void Configure(EntityTypeBuilder<RoutineSet> builder)
    {
        builder.ToTable(nameof(RoutineSet), WorkoutPlannerDbContextBase.SCHEMA);
        builder.HasKey(x => new { x.SetId, x.RoutineId });

        builder.Property(x => x.SetId);
        builder.Property(x => x.RoutineId);
        builder.Property(x => x.Order);

        // Foreing Key
        builder.HasOne(x => x.Set)
            .WithMany(x => x.RoutineSets)
            .HasForeignKey(x => x.SetId);

        builder.HasOne(x => x.Routine)
            .WithMany(x => x.RoutineSets)
            .HasForeignKey(x => x.RoutineId);
    }
}