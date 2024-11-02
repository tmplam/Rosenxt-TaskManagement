using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Abstractions;

public interface IPasswordService
{
    public string HashPassword(User user, string password);
    public bool VerifyPassword(User user, string enteredPassword);
}
