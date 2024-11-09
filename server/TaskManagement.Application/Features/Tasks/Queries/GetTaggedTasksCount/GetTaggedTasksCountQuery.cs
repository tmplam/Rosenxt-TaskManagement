using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Tasks.Queries.GetTaggedTasksCount;

public record GetTaggedTasksCountQuery() : IQuery<GetTaggedTasksCountResult>;

public record GetTaggedTasksCountResult(int Count);