using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutPlanner.Domain.Entities;
using WorkoutPlanner.Persistence.Infrastructure;

namespace WorkoutPlanner.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User), WorkoutPlannerDbContextBase.SCHEMA);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWID()");
        builder.Property(x => x.UserName)
            .HasMaxLength(50);
        builder.Property(x => x.Email)
            .HasMaxLength(150);
        builder.Property(x => x.Password);
        builder.Property(x => x.BirthDay)
            .HasColumnType("date")
            .HasConversion<DateOnlyConverter, DateOnlyComparer>();

        //Index
        builder.HasIndex(x => x.UserName)
            .IsUnique();
        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}
