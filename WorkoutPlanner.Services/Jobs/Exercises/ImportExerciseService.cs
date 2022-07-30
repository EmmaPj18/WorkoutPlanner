using Microsoft.Extensions.Logging;
using WorkoutPlanner.Common.Extensions;
using WorkoutPlanner.Infrastructure.Exercise.Contracts;
using WorkoutPlanner.Infrastructure.Exercise.Interfaces;
using WorkoutPlanner.Services.Jobs.Interfaces;

namespace WorkoutPlanner.Services.Jobs.Exercises;

public class HttpImportExerciseService : IImportExerciseService
{
    private readonly IExerciseClient _exerciseClient;
    private readonly IReadWriteWorkoutPlannerDbContext _readWriteDbContext;
    private readonly ILogger<HttpImportExerciseService> _logger;

    public HttpImportExerciseService(
        IExerciseClient exerciseClient,
        IReadWriteWorkoutPlannerDbContext readWriteDbContext,
        ILogger<HttpImportExerciseService> logger)
    {
        _exerciseClient = exerciseClient;
        _readWriteDbContext = readWriteDbContext;
        _logger = logger;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        var exerciseClientResult = await _exerciseClient.GetAll(cancellationToken);

        if (!exerciseClientResult.IsSuccess)
        {
            _logger.LogError("ExerciseClient not succedded. Message: {message}",
                exerciseClientResult.Message);

            throw new Exception($"ExerciseClient not succedded. Message: {exerciseClientResult.Message}");
        }

        await UpdateExerciseAsync(exerciseClientResult, cancellationToken);
        await ImportNewExercises(exerciseClientResult, cancellationToken);
    }

    private async Task ImportNewExercises(
        ExerciseResponse exerciseClientResult,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("{serviceName}.{method} started",
                   nameof(HttpImportExerciseService),
                   nameof(ImportNewExercises));

        foreach (var exerciseRecord in exerciseClientResult.Records)
        {
            _logger.LogInformation("{serviceName} importing new exercise with data [{exercise}]",
                    nameof(HttpImportExerciseService),
                    exerciseRecord.ToJson());

            var bodyPart = await _readWriteDbContext
                .BodyParts
                .Where(x => x.Name.ToLower() == exerciseRecord.BodyPart.ToLower())
                .FirstOrDefaultAsync(cancellationToken);

            var equipment = await _readWriteDbContext
                .Equipments
                .Where(x => x.Name.ToLower() == exerciseRecord.Equipment.ToLower())
                .FirstOrDefaultAsync(cancellationToken);

            var targetMuscle = await _readWriteDbContext
                .TargetMuscles
                .Where(x => x.Name.ToLower() == exerciseRecord.Target.ToLower())
                .FirstOrDefaultAsync(cancellationToken);

            var exercise = new Exercise
            {
                Name = exerciseRecord.Name,
                BodyPart = bodyPart,
                Equipment = equipment,
                TargetMuscle = targetMuscle,
            };

            await _readWriteDbContext.Exercises.AddAsync(exercise, cancellationToken);
        }

        await _readWriteDbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("{serviceName}.{method} finished",
                   nameof(HttpImportExerciseService),
                   nameof(ImportNewExercises));
    }

    private async Task UpdateExerciseAsync(
        ExerciseResponse exerciseClientResult,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("{serviceName}.{method} started",
                    nameof(HttpImportExerciseService),
                    nameof(UpdateExerciseAsync));

        var exerciseClientResultNames = exerciseClientResult.Records.Select(x => x.Name).ToList();

        var exercisesDb = await _readWriteDbContext.Exercises
                    .Include(x => x.TargetMuscle)
                    .Include(x => x.BodyPart)
                    .Include(x => x.Equipment)
                    .Where(x => exerciseClientResultNames.Contains(x.Name))
                    .ToListAsync(cancellationToken);

        if (exercisesDb is not null && exercisesDb.Count > 0)
        {
            foreach (var exercise in exercisesDb)
            {
                var exerciseRecord = exerciseClientResult.Records
                    .Where(x => x.Name.ToLower() == exercise.Name)
                    .First();

                _logger.LogInformation("{serviceName} updating exercise Id [{id}] with data [{exercise}]",
                    nameof(HttpImportExerciseService),
                    exercise.Id,
                    exerciseRecord.ToJson());

                if (exercise.TargetMuscle is null || exercise.TargetMuscle.Name.ToLower() != exerciseRecord.Target.ToLower())
                {
                    var targetMuscle = await _readWriteDbContext
                        .TargetMuscles
                        .Where(x => x.Name.ToLower() == exerciseRecord.Target.ToLower())
                        .FirstOrDefaultAsync(cancellationToken);

                    exercise.TargetMuscle = targetMuscle;
                }

                if (exercise.BodyPart is null || exercise.BodyPart.Name.ToLower() != exerciseRecord.BodyPart.ToLower())
                {
                    var bodyPart = await _readWriteDbContext
                        .BodyParts
                        .Where(x => x.Name.ToLower() == exerciseRecord.BodyPart.ToLower())
                        .FirstOrDefaultAsync(cancellationToken);

                    exercise.BodyPart = bodyPart;
                }

                if (exercise.Equipment is null || exercise.Equipment.Name.ToLower() != exerciseRecord.Equipment.ToLower())
                {
                    var equipment = await _readWriteDbContext
                        .Equipments
                        .Where(x => x.Name.ToLower() == exerciseRecord.Equipment.ToLower())
                        .FirstOrDefaultAsync(cancellationToken);

                    exercise.Equipment = equipment;
                }

                exerciseClientResult.Records.Remove(exerciseRecord);
            }

            await _readWriteDbContext.SaveChangesAsync(cancellationToken);
        }

        _logger.LogInformation("{serviceName}.{method} finished",
                    nameof(HttpImportExerciseService),
                    nameof(UpdateExerciseAsync));
    }
}
