using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Tasks.Queries.GetTasks;

public record GetTasksQuery() : IQuery<GetTasksResult>;
public record GetTasksResult();
