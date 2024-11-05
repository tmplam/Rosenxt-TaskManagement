using MediatR;
using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Exceptions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Tasks.Commands.DeleteTask;

public record DeleteTaskHandler(
    ITaskRepository _taskRepository,
    IUnitOfWork _unitOfWork,
    IClaimService _claimService) : ICommandHandler<DeleteTaskCommand>
{
    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null) throw new NotFoundException(nameof(TaskItem), request.Id);

        var userId = Guid.Parse(_claimService.GetUserId());
        if (userId != task.UserId) throw new UnauthorizedException("Unauthorized resource");

        _taskRepository.Delete(task);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
