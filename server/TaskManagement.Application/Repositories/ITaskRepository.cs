using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<List<TaskItem>> GetAllAsync(Expression<Func<TaskItem, bool>>? predicate = null);
    Task<List<(User User, List<TaskItem> DueTasks)>> GetAllDueTasksAsync();
    Task<TaskItem> AddAsync(TaskItem task);
    TaskItem Update(TaskItem task);
    TaskItem Delete(TaskItem task);
}
