using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutPlanner.Domain.Entities;

namespace WorkoutPlanner.Persistence.Configurations;

public class LookupConfiguration : IEntityTypeConfiguration<Lookup>
{
    public void Configure(EntityTypeBuilder<Lookup> builder)
    {
        builder.ToTable(nameof(Lookup), WorkoutPlannerDbContextBase.SCHEMA);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Category).HasMaxLength(64);
        builder.Property(x => x.Name).HasMaxLength(255);
        builder.Property(x => x.Description);
        builder.Property(x => x.IsActive);
        builder.Property(x => x.SortOrder);

        builder.HasIndex(x => new { x.Id, x.Category })
            .IsUnique();
        builder.HasIndex(x => new { x.Category, x.Name });
    }
}

