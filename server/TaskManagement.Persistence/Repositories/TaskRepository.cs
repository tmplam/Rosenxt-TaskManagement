using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Persistence.Repositories;

public class TaskRepository(ApplicationDbContext _dbContext) : ITaskRepository
{
    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        var task = await _dbContext.Set<TaskItem>().FirstOrDefaultAsync(task => task.Id == id);
        return task;
    }

    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        var addedTask = await _dbContext.Set<TaskItem>().AddAsync(task);
        return addedTask.Entity;
    }

    public TaskItem Update(TaskItem task)
    {
        var addedTask = _dbContext.Set<TaskItem>().Update(task);
        return addedTask.Entity;
    }
}
