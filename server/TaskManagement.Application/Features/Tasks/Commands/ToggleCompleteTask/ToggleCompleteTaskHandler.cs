using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Exceptions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Tasks.Commands.ToggleCompleteTask;

public sealed class ToggleCompleteTaskHandler(
    ITaskRepository _taskRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<ToggleCompleteTaskCommand, ToggleCompleteTaskResult>
{
    public async Task<ToggleCompleteTaskResult> Handle(ToggleCompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null) throw new NotFoundException(nameof(TaskItem), request.Id);

        task.IsCompleted = !task.IsCompleted;

        task = _taskRepository.Update(task);
        await _unitOfWork.SaveChangesAsync();

        return new ToggleCompleteTaskResult(task.Id);
    }
}
