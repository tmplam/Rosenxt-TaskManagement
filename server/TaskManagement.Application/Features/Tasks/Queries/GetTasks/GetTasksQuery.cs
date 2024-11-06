using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Dtos;

namespace TaskManagement.Application.Features.Tasks.Queries.GetTasks;

public record GetTasksQuery(bool? IsCompleted) : IQuery<GetTasksResult>;
public record GetTasksResult(List<TaskItemDto> Tasks);
