using FluentValidation;
using MediatR;
using System.Collections.Concurrent;

namespace WorkoutPlanner.Common.Validation;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private static readonly ConcurrentDictionary<Type, Type> _validatorTypeCache = new();
    private readonly IServiceProvider _serviceProvider;

    public ValidationBehavior(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var validators = GetValidators(request, _serviceProvider);

        if (validators is null || !validators.Any())
        {
            return next();
        }

        dynamic dtoRequest = request;

        IValidationContext context =
            _validatorTypeCache.TryGetValue(typeof(TRequest), out var validatorType)
            ? new ValidationContext<object>(dtoRequest.Model)
            : new ValidationContext<object>(request);

        var failures = validators
            .Select(x => x.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(x => x is not null)
            .ToList();

        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        return next();
    }

    private static IValidator[] GetValidators(TRequest request, IServiceProvider serviceProvider)
    {
        if (!_validatorTypeCache.TryGetValue(typeof(TRequest), out var validatorType))
        {
            var modelToValidate = request
                .GetType()
                .GetInterfaces()
                .FirstOrDefault(x => x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IDtoRequest<>));

            var genericValidatorType = typeof(IValidator<>);

            if (modelToValidate is null)
            {
                validatorType = genericValidatorType.MakeGenericType(new Type[] { request.GetType() });
            }
            else
            {
                var modelProperty = typeof(IDtoRequest<>).GetProperties().First();
                var typeOfModelToValidate = modelToValidate.GetProperty(modelProperty.Name);
                validatorType = genericValidatorType.MakeGenericType(new Type[] { typeOfModelToValidate!.PropertyType });

                if (validatorType is not null)
                {
                    _validatorTypeCache.TryAdd(typeof(TRequest), validatorType);
                }
            }
        }

        if (validatorType is not null)
        {
            var validator = (IValidator?)serviceProvider.GetService(validatorType);

            if (validator is not null)
            {
                return new[] { validator };
            }
        }

        return Array.Empty<IValidator>();
    }
}