using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Features.Tasks.Commands.CreateTask;

public sealed class CreateTaskHandler(
    ITaskRepository _taskRepository,
    IUnitOfWork _unitOfWork,
    IClaimService _claimService) : ICommandHandler<CreateTaskCommand, CreateTaskResult>
{
    public async Task<CreateTaskResult> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_claimService.GetUserId());
        var taskItem = TaskItem.Create(
            Guid.NewGuid(),
            command.Title,
            command.RemindBeforeDeadlineByMinutes,
            command.DueDate,
            userId);

        taskItem = await _taskRepository.AddAsync(taskItem);
        await _unitOfWork.SaveChangesAsync();

        return new CreateTaskResult(taskItem.Id);
    }
}
