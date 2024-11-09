using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Repositories;

public interface ITaskUserTagRepository
{
    Task<TaskUserTag> AddAsync(TaskUserTag taskUserTag);
    Task<TaskUserTag?> GetAsync(Expression<Func<TaskUserTag, bool>> predicate);
    Task<List<TaskUserTag>> GetAllAsync(Expression<Func<TaskUserTag, bool>> predicate);
    TaskUserTag Delete(TaskUserTag taskUserTag);
    Task<int> CountAsync(Expression<Func<TaskUserTag, bool>>? predicate);
}
