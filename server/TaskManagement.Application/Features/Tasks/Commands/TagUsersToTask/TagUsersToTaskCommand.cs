using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Tasks.Commands.TagUsersToTask;

public record TagUsersToTaskCommand(Guid TaskId, List<string> Emails) : ICommand<TagUsersToTaskResult>;

public record TagUsersToTaskResult(Guid TaskId);