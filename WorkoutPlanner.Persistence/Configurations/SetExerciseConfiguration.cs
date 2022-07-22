using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutPlanner.Domain.Entities;

namespace WorkoutPlanner.Persistence.Configurations;

public class SetExerciseConfiguration : IEntityTypeConfiguration<SetExercise>
{
    public void Configure(EntityTypeBuilder<SetExercise> builder)
    {
        builder.ToTable(nameof(SetExercise), WorkoutPlannerDbContextBase.SCHEMA);
        builder.HasKey(x => new { x.SetId, x.ExerciseId });

        builder.Property(x => x.SetId);
        builder.Property(x => x.ExerciseId);
        builder.Property(x => x.QuantityType);
        builder.Property(x => x.Quantity)
            .HasPrecision(18, 2);

        // Foreing Key
        builder.HasOne(x => x.Set)
            .WithMany(x => x.SetExercises)
            .HasForeignKey(x => x.SetId);

        builder.HasOne(x => x.Exercise)
            .WithMany(x => x.SetExercises)
            .HasForeignKey(x => x.ExerciseId);
    }
}
