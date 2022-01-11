using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalDoggy.BusinessLogic.Pipelines
{
    public class DoggyValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public DoggyValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var enumerableTasks = _validators.Select(validator => validator.ValidateAsync(context, cancellationToken));

            var validationResults = await Task.WhenAll(enumerableTasks);

            var failureList = validationResults
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();

            if (failureList.Any())
            {
                throw new ValidationException(failureList);
            }

            return await next();
        }
    }
}
