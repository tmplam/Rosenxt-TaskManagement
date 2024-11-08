using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
}
