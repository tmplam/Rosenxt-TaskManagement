using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Dtos;

namespace TaskManagement.Application.Features.Tasks.Queries.GetTaggedTasks;

public record GetTaggedTasksQuery() : IQuery<GetTaggedTasksResult>;

public record GetTaggedTasksResult(List<TaskItemDto> TaggedTasks);