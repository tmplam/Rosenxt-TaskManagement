using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Tasks.Commands.AcceptTaggedTask;

public record AcceptTaggedTaskCommand(Guid TaskId) : ICommand<AcceptTaggedTaskResult>;

public record AcceptTaggedTaskResult(Guid TaskId);