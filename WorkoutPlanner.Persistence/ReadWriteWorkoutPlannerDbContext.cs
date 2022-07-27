﻿using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Services.Interfaces;

namespace WorkoutPlanner.Persistence;

public class ReadWriteWorkoutPlannerDbContext : WorkoutPlannerDbContextBase, IReadWriteWorkoutPlannerDbContext
{
    public ReadWriteWorkoutPlannerDbContext(DbContextOptions<ReadWriteWorkoutPlannerDbContext> option, IEnumerable<IDataSeedService> dataSeedServices)
        : base(options, dataSeedServices)
    {

    }
}
