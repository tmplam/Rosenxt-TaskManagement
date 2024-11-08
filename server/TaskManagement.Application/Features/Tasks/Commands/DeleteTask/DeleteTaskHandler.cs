using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Exceptions;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Features.Tasks.Commands.DeleteTask;

public record DeleteTaskHandler(
    ITaskRepository _taskRepository,
    IUnitOfWork _unitOfWork,
    IClaimService _claimService) : ICommandHandler<DeleteTaskCommand, DeleteTaskResult>
{
    public async Task<DeleteTaskResult> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(command.Id);
        if (task == null) throw new NotFoundException(nameof(TaskItem), command.Id);

        var userId = Guid.Parse(_claimService.GetUserId());
        if (userId != task.UserId) throw new UnauthorizedException("Unauthorized resource");

        task = _taskRepository.Delete(task);
        await _unitOfWork.SaveChangesAsync();

        return new DeleteTaskResult(task.Id);
    }
}
