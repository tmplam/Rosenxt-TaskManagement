using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Features.Tasks.Commands.DeclineTaggedTask;

public sealed class DeclineTaggedTaskHandler(
    ITaskUserTagRepository _taskUserTagRepository,
    IUnitOfWork _unitOfWork,
    IClaimService _claimService) : ICommandHandler<DeclineTaggedTaskCommand, DeclineTaggedTaskResult>
{
    public async Task<DeclineTaggedTaskResult> Handle(DeclineTaggedTaskCommand command, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_claimService.GetUserId());
        var tag = await _taskUserTagRepository.GetAsync(tag => tag.UserId == userId && tag.TaskId == command.TaskId);

        if (tag == null) throw new BadRequestException($"No tag with task id {command} tagged to you");

        _taskUserTagRepository.Delete(tag);
        await _unitOfWork.SaveChangesAsync();

        return new DeclineTaggedTaskResult(command.TaskId);
    }
}
