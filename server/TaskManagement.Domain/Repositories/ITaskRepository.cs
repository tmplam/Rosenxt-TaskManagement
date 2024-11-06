using System.Linq.Expressions;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<List<TaskItem>> GetAllAsync(Expression<Func<TaskItem, bool>>? predicate = null);
    Task<TaskItem> AddAsync(TaskItem task);
    TaskItem Update(TaskItem task);
    TaskItem Delete(TaskItem task);
}
