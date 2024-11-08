using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Features.Tasks.Commands.TagUsersToTask;

public sealed class TagUsersToTaskHandler(
    IUserRepository _userRepository,
    ITaskRepository _taskRepository,
    ITaskUserTagRepository _taskUserTagRepository,
    IClaimService _claimService,
    IUnitOfWork _unitOfWork) : ICommandHandler<TagUsersToTaskCommand, TagUsersToTaskResult>
{
    public async Task<TagUsersToTaskResult> Handle(TagUsersToTaskCommand command, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(user => command.Emails.Contains(user.Email));
        var task = await _taskRepository.GetByIdAsync(command.TaskId);
        if (task is null) throw new BadRequestException("Task does not exist");

        var userIdList = users.Select(user => user.Id).ToList();
        var existingUserIds = (await _taskUserTagRepository.GetAllAsync(tag => tag.TaskId == command.TaskId && userIdList.Contains(tag.UserId))).Select(tag => tag.UserId);

        var taskOwnerId = Guid.Parse(_claimService.GetUserId());
        foreach (var user in users)
        {
            if (existingUserIds.Contains(user.Id) || user.Id == taskOwnerId)
            {
                continue;
            }
            var tag = TaskUserTag.Create(Guid.NewGuid(), command.TaskId, userId: user.Id);
            await _taskUserTagRepository.AddAsync(tag);
        }
        await _unitOfWork.SaveChangesAsync();

        return new TagUsersToTaskResult(command.TaskId);
    }
}
