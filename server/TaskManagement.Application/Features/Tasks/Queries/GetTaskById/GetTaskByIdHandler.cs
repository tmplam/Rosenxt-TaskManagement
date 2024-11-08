using Mapster;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Exceptions;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Features.Tasks.Queries.GetTaskById;

public class GetTaskByIdHandler(ITaskRepository _taskRepository) : IQueryHandler<GetTaskByIdQuery, GetTaskByIdResult>
{
    public async Task<GetTaskByIdResult> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(query.Id);
        if (task == null) throw new NotFoundException(nameof(TaskItem), query.Id);

        var taskDto = task.Adapt<TaskItemDto>();
        return new GetTaskByIdResult(taskDto);
    }
}
