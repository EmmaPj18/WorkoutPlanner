using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutPlanner.Domain.Entities;

namespace WorkoutPlanner.Persistence.Configurations;

public class RoutineConfiguration : IEntityTypeConfiguration<Routine>
{
    public void Configure(EntityTypeBuilder<Routine> builder)
    {
        builder.ToTable(nameof(Routine), WorkoutPlannerDbContextBase.SCHEMA);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn(1, 1);

        builder.Property(x => x.Description);
        builder.Property(x => x.ExplainVideoUrl)
            .HasMaxLength(200);
        builder.Property(x => x.DateCreated)
            .HasDefaultValueSql();
    }
}
