using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Exceptions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Tasks.Commands.UpdateTask;

public sealed class UpdateTaskHandler(
    ITaskRepository _taskRepository,
    IUnitOfWork _unitOfWork,
    IClaimService _claimService) : ICommandHandler<UpdateTaskCommand, UpdateTaskResult>
{
    public async Task<UpdateTaskResult> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(command.Id);
        if (task == null) throw new NotFoundException(nameof(TaskItem), command.Id);

        var userId = Guid.Parse(_claimService.GetUserId());
        if (userId != task.UserId) throw new UnauthorizedException("Unauthorized resource");

        task.Title = command.Title;
        task.Description = command.Description;
        task.RemindBeforeDeadlineByMinutes = command.RemindBeforeDeadlineByMinutes;
        task.DueDate = command.DueDate;

        task = _taskRepository.Update(task);
        await _unitOfWork.SaveChangesAsync();

        return new UpdateTaskResult(task.Id);
    }
}
