using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Tasks.Commands.DeclineTaggedTask;

public record DeclineTaggedTaskCommand(Guid TaskId) : ICommand<DeclineTaggedTaskResult>;

public record DeclineTaggedTaskResult(Guid TaskId);