using System.Linq.Expressions;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync(Expression<Func<User, bool>> predicate);
    Task<List<string>> GetEmailListAsync(string keyword, int? topN = 5);
    Task<User> AddAsync(User user);
}
