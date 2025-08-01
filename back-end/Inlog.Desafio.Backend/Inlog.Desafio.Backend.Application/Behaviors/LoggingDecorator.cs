using Inlog.Desafio.Backend.Application.Messaging;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Inlog.Desafio.Backend.Application.Behaviors;

internal static class LoggingDecorator
{
    internal sealed class CommandBaseHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _inner;
        private readonly ILogger<CommandBaseHandler<TCommand>> _logger;

        public CommandBaseHandler(ICommandHandler<TCommand> inner,
            ILogger<CommandBaseHandler<TCommand>> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing command {CommandName}", typeof(TCommand).Name);

            var result = await _inner.Handle(command, cancellationToken);

            if (result.IsSuccess)
                _logger.LogInformation("Completed command {CommandName}", typeof(TCommand).Name);
            else
                _logger.LogError("Completed command {CommandName} with error", typeof(TCommand).Name);

            return result;
        }
    }

    internal sealed class CommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        private readonly ICommandHandler<TCommand, TResponse> _inner;
        private readonly ILogger<CommandHandler<TCommand, TResponse>> _logger;

        public CommandHandler(ICommandHandler<TCommand, TResponse> inner,
            ILogger<CommandHandler<TCommand, TResponse>> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing command {CommandName}", typeof(TCommand).Name);

            var result = await _inner.Handle(command, cancellationToken);

            if (result.IsSuccess)
                _logger.LogInformation("Completed command {CommandName}", typeof(TCommand).Name);
            else
                _logger.LogError("Completed command {CommandName} with error", typeof(TCommand).Name);

            return result;
        }
    }

    internal sealed class QueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        private readonly IQueryHandler<TQuery, TResponse> _inner;
        private readonly ILogger<QueryHandler<TQuery, TResponse>> _logger;

        public QueryHandler(IQueryHandler<TQuery, TResponse> inner,
            ILogger<QueryHandler<TQuery, TResponse>> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing query {QueryName}", typeof(TQuery).Name);

            var result = await _inner.Handle(query, cancellationToken);

            if (result.IsSuccess)
                _logger.LogInformation("Completed query {QueryName}", typeof(TQuery).Name);
            else
                _logger.LogError("Completed query {QueryName} with error", typeof(TQuery).Name);

            return result;
        }
    }
}