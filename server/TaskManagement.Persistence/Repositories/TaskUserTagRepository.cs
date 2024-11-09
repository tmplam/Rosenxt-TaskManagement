using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Persistence.Repositories;

public class TaskUserTagRepository(ApplicationDbContext _dbContext) : ITaskUserTagRepository
{
    public async Task<TaskUserTag> AddAsync(TaskUserTag taskUserTag)
    {
        var addedTag = await _dbContext.Set<TaskUserTag>().AddAsync(taskUserTag);
        return addedTag.Entity;
    }

    public async Task<int> CountAsync(Expression<Func<TaskUserTag, bool>>? predicate)
    {
        var tags = _dbContext.Set<TaskUserTag>().AsQueryable();
        if (predicate is not null)
        {
            tags = tags.Where(predicate);
        }    
        return await tags.CountAsync();
    }

    public TaskUserTag Delete(TaskUserTag taskUserTag)
    {
        var removeTag = _dbContext.Set<TaskUserTag>().Remove(taskUserTag);
        return removeTag.Entity;
    }

    public async Task<List<TaskUserTag>> GetAllAsync(Expression<Func<TaskUserTag, bool>> predicate)
    {
        var tags = await _dbContext.Set<TaskUserTag>().Where(predicate).ToListAsync();
        return tags;
    }

    public async Task<TaskUserTag?> GetAsync(Expression<Func<TaskUserTag, bool>> predicate)
    {
        var tag = await _dbContext.Set<TaskUserTag>().FirstOrDefaultAsync(predicate);
        return tag;
    }
}
