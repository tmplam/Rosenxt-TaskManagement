using Mapster;
using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Features.Tasks.Queries.GetTaggedTasks;

public sealed class GetTaggedTasksHandler(
    ITaskRepository _taskRepository,
    ITaskUserTagRepository _taskUserTagRepository,
    IClaimService _claimService) : IQueryHandler<GetTaggedTasksQuery, GetTaggedTasksResult>
{
    public async Task<GetTaggedTasksResult> Handle(GetTaggedTasksQuery query, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_claimService.GetUserId());
        var taggedTaskIds = (await _taskUserTagRepository.GetAllAsync(tag => tag.UserId == userId)).Select(tag => tag.TaskId);

        var taggedTaskList = await _taskRepository.GetAllAsync(task => taggedTaskIds.Contains(task.Id), includeUser: true);

        var result = taggedTaskList.Adapt<List<TaskItemDto>>();

        return new GetTaggedTasksResult(result);
    }
}
