using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutPlanner.Domain.Entities;

namespace WorkoutPlanner.Persistence.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable(nameof(Exercise), WorkoutPlannerDbContextBase.SCHEMA);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn(1, 1);

        builder.Property(x => x.Description);
        builder.Property(x => x.ExplainVideoUrl)
            .HasMaxLength(200);
        builder.Property(x => x.Name)
            .HasMaxLength(150);
        builder.Property(x => x.EquipmentId);
        builder.Property(x => x.BodyPartId);
        builder.Property(x => x.TargetMuscleId);

        // ForeignKey
        builder.HasOne(x => x.Equipment)
            .WithMany(x => x.Exercises)
            .HasForeignKey(x => x.EquipmentId);

        builder.HasOne(x => x.BodyPart)
            .WithMany(x => x.Exercises)
            .HasForeignKey(x => x.BodyPartId);

        builder.HasOne(x => x.TargetMuscle)
            .WithMany(x => x.Exercises)
            .HasForeignKey(x => x.TargetMuscleId);

        // Index
        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}