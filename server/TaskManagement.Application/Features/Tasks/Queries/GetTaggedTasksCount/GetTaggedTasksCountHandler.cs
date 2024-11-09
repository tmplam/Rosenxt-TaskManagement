using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Features.Tasks.Queries.GetTaggedTasksCount;

public sealed class GetTaggedTasksCountHandler(
    ITaskUserTagRepository _taskUserTagRepository,
    IClaimService _claimService) : IQueryHandler<GetTaggedTasksCountQuery, GetTaggedTasksCountResult>
{
    public async Task<GetTaggedTasksCountResult> Handle(GetTaggedTasksCountQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_claimService.GetUserId());
        var count = await _taskUserTagRepository.CountAsync(tag => tag.UserId == userId);
        return new GetTaggedTasksCountResult(count);
    }
}
