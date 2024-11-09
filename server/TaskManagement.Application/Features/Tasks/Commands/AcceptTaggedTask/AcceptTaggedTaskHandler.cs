using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Features.Tasks.Commands.AcceptTaggedTask;

public sealed class AcceptTaggedTaskHandler(
    ITaskRepository _taskRepository,
    ITaskUserTagRepository _taskUserTagRepository,
    IUnitOfWork _unitOfWork,
    IClaimService _claimService) : ICommandHandler<AcceptTaggedTaskCommand, AcceptTaggedTaskResult>
{
    public async Task<AcceptTaggedTaskResult> Handle(AcceptTaggedTaskCommand command, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_claimService.GetUserId());

        var tag = await _taskUserTagRepository.GetAsync(tag => tag.UserId == userId && tag.TaskId == command.TaskId);
        if (tag == null) throw new BadRequestException($"No tag with task id {command} tagged to you");

        var task = await _taskRepository.GetByIdAsync(command.TaskId);
        if (task == null) throw new NotFoundException(nameof(TaskItem), command.TaskId);

        var newTask = TaskItem.Create(Guid.NewGuid(), task.Title, task.RemindBeforeDeadlineByMinutes, task.DueDate, userId);

        newTask = await _taskRepository.AddAsync(newTask);
        _taskUserTagRepository.Delete(tag);
        await _unitOfWork.SaveChangesAsync();

        return new AcceptTaggedTaskResult(newTask.Id);
    }
}
