using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Persistence.Repositories;

public class UserRepository(ApplicationDbContext _dbContext) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }

    public async Task<User> AddAsync(User user)
    {
        var addedUser = await _dbContext.Set<User>().AddAsync(user);
        return addedUser.Entity;
    }
}
