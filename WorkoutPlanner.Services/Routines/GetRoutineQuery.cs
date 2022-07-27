namespace WorkoutPlanner.Services.Routines;

public class GetRoutineQuery : IRequest<RoutineResponse>, IDtoRequest<GetRoutineRequest>
{
    public GetRoutineRequest Model { get; set; } = default!;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, SetExerciseResponse>()
                .ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Name))
                .ForMember(src => src.ExplainVideoUrl, opt => opt.MapFrom(dest => dest.ExplainVideoUrl))
                .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.Description));

            CreateMap<SetExercise, SetExerciseResponse>()
                .ForMember(src => src.Order, opt => opt.MapFrom(dest => dest.Order))
                .ForMember(src => src.Quantity, opt => opt.MapFrom(dest => dest.Quantity))
                .ForMember(src => src.QuantityType, opt => opt.MapFrom(dest => dest.QuantityType))
                .ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Exercise))
                .ForMember(src => src.ExplainVideoUrl, opt => opt.MapFrom(dest => dest.Exercise))
                .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.Exercise));

            CreateMap<Set, RoutineSetResponse>()
                .ForMember(src => src.Exercises, opt => opt.MapFrom(dest => dest.SetExercises))
                .ForMember(src => src.ExplainVideoUrl, opt => opt.MapFrom(dest => dest.ExplainVideoUrl))
                .ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Name))
                .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.Description))
                .ForMember(src => src.RoundsNumber, opt => opt.MapFrom(dest => dest.RoundsNumber))
                .ForMember(src => src.SetType, opt => opt.MapFrom(dest => dest.Type));

            CreateMap<RoutineSet, RoutineSetResponse>()
                .ForMember(src => src.Order, opt => opt.MapFrom(dest => dest.Order))
                .ForMember(src => src.ExplainVideoUrl, opt => opt.MapFrom(dest => dest.Set))
                .ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Set))
                .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.Set))
                .ForMember(src => src.RoundsNumber, opt => opt.MapFrom(dest => dest.Set))
                .ForMember(src => src.SetType, opt => opt.MapFrom(dest => dest.Set));

            CreateMap<Routine, RoutineResponse>()
                .ForMember(src => src.Sets, opt => opt.MapFrom(dest => dest.RoutineSets));
        }
    }

    public class Validator : AbstractValidator<GetRoutineRequest>
    {
        public Validator()
        {
            RuleFor(x => x.RoutineId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Must(x => x != Guid.Empty);
        }
    }


    public class Handler : IRequestHandler<GetRoutineQuery, RoutineResponse>
    {
        private readonly IReadOnlyWorkoutPlannerDbContext _readOnlyDbContext;
        private readonly IMapper _mapper;

        public Handler(IReadOnlyWorkoutPlannerDbContext readOnlyDbContext, IMapper mapper)
        {
            _readOnlyDbContext = readOnlyDbContext;
            _mapper = mapper;
        }

        public async Task<RoutineResponse> Handle(GetRoutineQuery req, CancellationToken cancellationToken)
        {
            var request = req.Model;

            var query = _readOnlyDbContext.Routines
                .Include(x => x.RoutineSets)
                .ThenInclude(x => x.Set)
                .ThenInclude(x => x.SetExercises)
                .ThenInclude(x => x.Exercise)
                .AsQueryable()
                .ByRoutineId(request.RoutineId);

            var data = await query.FirstOrDefaultAsync(cancellationToken);

            var response = _mapper.Map<RoutineResponse>(data);

            return response;
        }
    }
}

public static class GetRoutineQueryExtensions
{
    public static IQueryable<Routine> ByRoutineId(this IQueryable<Routine> query, Guid id)
    {
        return query.Where(x => x.Id == id);
    }
}
