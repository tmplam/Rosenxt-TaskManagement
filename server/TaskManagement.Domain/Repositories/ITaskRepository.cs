using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<TaskItem> AddAsync(TaskItem task);
    TaskItem Update(TaskItem task);
}
