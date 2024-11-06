using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Exceptions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Tasks.Commands.ToggleCompleteTask;

public sealed class ToggleCompleteTaskHandler(
    ITaskRepository _taskRepository,
    IUnitOfWork _unitOfWork,
    IClaimService _claimService) : ICommandHandler<ToggleCompleteTaskCommand, ToggleCompleteTaskResult>
{
    public async Task<ToggleCompleteTaskResult> Handle(ToggleCompleteTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(command.Id);
        if (task == null) throw new NotFoundException(nameof(TaskItem), command.Id);

        var userId = Guid.Parse(_claimService.GetUserId());
        if (userId != task.UserId) throw new UnauthorizedException("Unauthorized resource");

        task.IsCompleted = !task.IsCompleted;

        task = _taskRepository.Update(task);
        await _unitOfWork.SaveChangesAsync();

        return new ToggleCompleteTaskResult(task.Id);
    }
}
