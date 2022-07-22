using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Common.Extensions;
using WorkoutPlanner.Domain.Entities;
using WorkoutPlanner.Domain.Enums;
using WorkoutPlanner.Services.Interfaces;

namespace WorkoutPlanner.Persistence.DataSeeds;

public class LookupSeeds : IDataSeedService
{
    public readonly Lookup[] SetTypeSeeds = Enum.GetValues<SetType>()
        .Select((x, i) => new Lookup
        {
            Category = nameof(SetType),
            Id = (int)x,
            IsActive = true,
            Name = x.ToString(),
            SortOrder = i,
            Description = x.GetDescription()
        }).ToArray();

    public readonly Lookup[] QuantityTypeSeeds = Enum.GetValues<QuantityType>()
        .Select((x, i) => new Lookup
        {
            Category = nameof(QuantityType),
            Id = (int)x,
            IsActive = true,
            Name = x.ToString(),
            SortOrder = i,
            Description = x.GetDescription()
        }).ToArray();

    public void Seed(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Lookup>().HasData(SetTypeSeeds);
        modelBuilder.Entity<Lookup>().HasData(QuantityTypeSeeds);
    }
}
