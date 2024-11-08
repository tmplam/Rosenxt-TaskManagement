using MediatR;
using Quartz;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.DomainEvents;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.BackgroundJobs;

public sealed class ProcessRemindDueTasksJob(
    ITaskRepository _taskRepository,
    IUnitOfWork _unitOfWork,
    IPublisher _publisher) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var userDueTasksList = await _taskRepository.GetAllDueTasksAsync();

        foreach (var userDueTasks in  userDueTasksList)
        {
            var remindDueTaskDomainEvent = new RemindDueTaskDomainEvent(Guid.NewGuid(), userDueTasks.User, userDueTasks.DueTasks);

            await _publisher.Publish(remindDueTaskDomainEvent, context.CancellationToken);

            MarkTasksAsNotified(userDueTasks.DueTasks);
        }

        await _unitOfWork.SaveChangesAsync(context.CancellationToken);
    }

    private void MarkTasksAsNotified(List<TaskItem> tasks)
    {
        foreach (var task in tasks)
        {
            task.IsNotified = true;
        }
    }
}
