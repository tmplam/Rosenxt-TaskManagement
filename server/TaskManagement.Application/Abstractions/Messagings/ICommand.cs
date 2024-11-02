using MediatR;

namespace TaskManagement.Application.Abstractions.Messagings;

public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
