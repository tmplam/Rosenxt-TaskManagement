using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Abstractions;

public interface IJwtProvider
{
    string Generate(User user);
}
