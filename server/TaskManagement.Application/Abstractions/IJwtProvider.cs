using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Abstractions;

public interface IJwtProvider
{
    string GenerateToken(User user);
}
