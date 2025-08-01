using SharedKernel;

namespace Inlog.Desafio.Backend.Application.Messaging;

public interface ICommand : ICommand<Result> { }

public interface ICommand<TResponse> : IBaseCommand { }

public interface IBaseCommand { }