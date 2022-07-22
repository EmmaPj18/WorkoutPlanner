using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutPlanner.Domain.Entities;

namespace WorkoutPlanner.Persistence.Configurations;

public class SetConfiguration : IEntityTypeConfiguration<Set>
{
    public void Configure(EntityTypeBuilder<Set> builder)
    {
        builder.ToTable(nameof(Set), WorkoutPlannerDbContextBase.SCHEMA);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn(1, 1);

        builder.Property(x => x.Description);
        builder.Property(x => x.ExplainVideoUrl)
            .HasMaxLength(200);
        builder.Property(x => x.Name)
            .HasMaxLength(150);
        builder.Property(x => x.Type);
        builder.Property(x => x.RoundsNumber);

        // Index
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Type);
    }
}
