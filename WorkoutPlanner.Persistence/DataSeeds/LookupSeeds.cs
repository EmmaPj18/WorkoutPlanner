using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Common.Extensions;
using WorkoutPlanner.Domain.Entities;
using WorkoutPlanner.Domain.Enums;
using WorkoutPlanner.Services.Interfaces;

namespace WorkoutPlanner.Persistence.DataSeeds;

public class LookupSeeds : IDataSeedService
{
    private readonly Lookup[] SetTypeSeeds = Enum.GetValues<SetType>()
        .Select((enumValue, index) => new Lookup
        {
            Category = nameof(SetType),
            Id = (int)enumValue,
            IsActive = true,
            Name = enumValue.ToString(),
            SortOrder = index,
            Description = enumValue.GetDescription()
        }).ToArray();

    private readonly Lookup[] QuantityTypeSeeds = Enum.GetValues<QuantityType>()
        .Select((enumValue, index) => new Lookup
        {
            Category = nameof(QuantityType),
            Id = (int)enumValue,
            IsActive = true,
            Name = enumValue.ToString(),
            SortOrder = index,
            Description = enumValue.GetDescription()
        }).ToArray();

    public void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lookup>().HasData(SetTypeSeeds);
        modelBuilder.Entity<Lookup>().HasData(QuantityTypeSeeds);
    }
}
