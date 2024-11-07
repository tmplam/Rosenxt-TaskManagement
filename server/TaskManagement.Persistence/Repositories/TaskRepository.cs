using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Persistence.Repositories;

public class TaskRepository(ApplicationDbContext _dbContext) : ITaskRepository
{
    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        var task = await _dbContext.Set<TaskItem>()
            .Include(task => task.TaggedUsers)
            .FirstOrDefaultAsync(task => task.Id == id);
        return task;
    }

    public async Task<List<TaskItem>> GetAllAsync(Expression<Func<TaskItem, bool>>? predicate = null)
    {
        var tasks = _dbContext.Set<TaskItem>().AsQueryable();
        if (predicate is not null)
        {
            tasks = tasks.Where(predicate);
        }
        tasks = tasks.OrderByDescending(task => task.CreatedAt);
        return await tasks.ToListAsync();
    }

    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        var addedTask = await _dbContext.Set<TaskItem>().AddAsync(task);
        return addedTask.Entity;
    }

    public TaskItem Update(TaskItem task)
    {
        var updatedTask = _dbContext.Set<TaskItem>().Update(task);
        return updatedTask.Entity;
    }

    public TaskItem Delete(TaskItem task)
    {
        var deletedTask = _dbContext.Set<TaskItem>().Remove(task);
        return deletedTask.Entity;
    }
}
