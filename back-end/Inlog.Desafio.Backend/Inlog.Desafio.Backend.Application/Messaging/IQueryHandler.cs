using SharedKernel;

namespace Inlog.Desafio.Backend.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}