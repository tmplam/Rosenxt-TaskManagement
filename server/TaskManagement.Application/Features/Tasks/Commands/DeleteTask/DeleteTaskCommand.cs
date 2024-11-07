using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Tasks.Commands.DeleteTask;

public record DeleteTaskCommand(Guid Id) : ICommand<DeleteTaskResult>;
public record DeleteTaskResult(Guid Id);
