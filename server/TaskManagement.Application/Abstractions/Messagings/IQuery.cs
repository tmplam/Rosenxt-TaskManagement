using MediatR;

namespace TaskManagement.Application.Abstractions.Messagings;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
}
