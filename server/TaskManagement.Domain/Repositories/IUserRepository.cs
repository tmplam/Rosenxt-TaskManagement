using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
}
