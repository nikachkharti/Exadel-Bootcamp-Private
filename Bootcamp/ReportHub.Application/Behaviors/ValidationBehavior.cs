using FluentValidation;
using MediatR;
using ReportHub.Application.Contracts.CQRS;
using System.ComponentModel.DataAnnotations;
using ValidationException = FluentValidation.ValidationException;

namespace ReportHub.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validatorResults = await Task
                .WhenAll(_validators
                .Select(v => v
                .ValidateAsync(context, cancellationToken)));

            var failures =
                validatorResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);

            return await next();
        }
    }
}
