using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutPlanner.Domain.Entities;

namespace WorkoutPlanner.Persistence.Configurations;

public class UserRoutineConfiguration : IEntityTypeConfiguration<UserRoutine>
{
    public void Configure(EntityTypeBuilder<UserRoutine> builder)
    {
        builder.ToTable(nameof(UserRoutine), WorkoutPlannerDbContextBase.SCHEMA);
        builder.HasKey(x => new { x.UserId, x.RoutineId });

        builder.Property(x => x.UserId);
        builder.Property(x => x.RoutineId);
        builder.Property(x => x.WorkOutDate)
            .HasDefaultValueSql();

        // Foreing Key
        builder.HasOne(x => x.User)
            .WithMany(x => x.UserRoutines)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Routine)
            .WithMany(x => x.UserRoutines)
            .HasForeignKey(x => x.RoutineId);
    }
}
