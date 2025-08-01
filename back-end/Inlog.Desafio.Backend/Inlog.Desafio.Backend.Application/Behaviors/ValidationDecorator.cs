using FluentValidation;
using FluentValidation.Results;
using Inlog.Desafio.Backend.Application.Messaging;
using SharedKernel;

namespace Inlog.Desafio.Backend.Application.Behaviors;

internal static class ValidationDecorator
{
    private static ValidationError CreateValidationError(ValidationFailure[] validationFailures)
    {
        return new ValidationError(validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage))
            .ToArray());
    }

    internal sealed class CommandBaseHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _inner;
        private readonly IEnumerable<IValidator<TCommand>> _validators;

        public CommandBaseHandler(ICommandHandler<TCommand> inner,
            IEnumerable<IValidator<TCommand>> validators)
        {
            _inner = inner;
            _validators = validators;
        }

        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TCommand>(command);
            ValidationResult[] validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            ValidationFailure[] failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToArray();

            if (failures.Length != 0) return Result.Failure(CreateValidationError(failures));

            return await _inner.Handle(command, cancellationToken);
        }
    }

    internal sealed class CommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        private readonly ICommandHandler<TCommand, TResponse> _inner;
        private readonly IEnumerable<IValidator<TCommand>> _validators;

        public CommandHandler(ICommandHandler<TCommand, TResponse> inner,
            IEnumerable<IValidator<TCommand>> validators)
        {
            _inner = inner;
            _validators = validators;
        }

        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TCommand>(command);
            ValidationResult[] validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            ValidationFailure[] failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToArray();

            if (failures.Length != 0) return Result.Failure<TResponse>(CreateValidationError(failures));

            return await _inner.Handle(command, cancellationToken);
        }
    }
}