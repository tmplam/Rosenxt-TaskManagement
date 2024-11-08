using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Repositories;
using System.Linq.Expressions;

namespace TaskManagement.Persistence.Repositories;

public class UserRepository(ApplicationDbContext _dbContext) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Email == email);
        return user;
    }

    public async Task<User> AddAsync(User user)
    {
        var addedUser = await _dbContext.Set<User>().AddAsync(user);
        return addedUser.Entity;
    }

    public async Task<List<string>> GetEmailListAsync(string keyword, int? topN = 5)
    {
        var emailList = await _dbContext.Set<User>()
            .Where(user => user.Email.Contains(keyword))
            .Take(topN ?? 5)
            .Select(user => user.Email)
            .ToListAsync();
        return emailList;
    }

    public async Task<List<User>> GetAllAsync(Expression<Func<User, bool>> predicate)
    {
        return await _dbContext.Set<User>().Where(predicate).ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Id == id);
        return user;
    }
}
