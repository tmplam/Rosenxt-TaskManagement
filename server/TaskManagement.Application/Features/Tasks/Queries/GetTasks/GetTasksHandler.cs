using Mapster;
using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Features.Tasks.Queries.GetTasks;

public class GetTasksHandler(
    ITaskRepository _taskRepository,
    IClaimService _claimService) : IQueryHandler<GetTasksQuery, GetTasksResult>
{
    public async Task<GetTasksResult> Handle(GetTasksQuery query, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_claimService.GetUserId());
        var tasks = await _taskRepository.GetAllAsync(task => task.UserId == userId && (query.IsCompleted == null || task.IsCompleted == query.IsCompleted));
        var taskDtos = tasks.Adapt<List<TaskItemDto>>();
        
        return new GetTasksResult(taskDtos);
    }
}
