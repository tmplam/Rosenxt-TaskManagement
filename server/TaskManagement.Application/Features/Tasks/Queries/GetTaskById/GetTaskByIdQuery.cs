using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Dtos;

namespace TaskManagement.Application.Features.Tasks.Queries.GetTaskById;

public record GetTaskByIdQuery(Guid Id) : IQuery<GetTaskByIdResult>;
public record GetTaskByIdResult(TaskItemDto Task);