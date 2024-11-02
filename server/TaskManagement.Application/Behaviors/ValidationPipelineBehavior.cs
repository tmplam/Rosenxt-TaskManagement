using FluentValidation;
using MediatR;
using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Behaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults.Where(r => r.Errors.Any()).SelectMany(r => r.Errors).ToList();

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}