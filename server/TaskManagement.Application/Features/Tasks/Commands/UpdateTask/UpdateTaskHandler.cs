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
    public async Task<UpdateTaskResult> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null) throw new NotFoundException(nameof(TaskItem), request.Id);

        var userId = Guid.Parse(_claimService.GetUserId());
        if (userId != task.UserId) throw new UnauthorizedException("Unauthorized resource");

        task.Title = request.Title;
        task.Description = request.Description;
        task.RemindBeforeDeadlineByMinutes = request.RemindBeforeDeadlineByMinutes;
        task.DueDate = request.DueDate;

        task = _taskRepository.Update(task);
        await _unitOfWork.SaveChangesAsync();

        return new UpdateTaskResult(task.Id);
    }
}
