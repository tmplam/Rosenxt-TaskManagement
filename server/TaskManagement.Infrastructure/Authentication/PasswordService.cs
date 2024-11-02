using Microsoft.AspNetCore.Identity;
using TaskManagement.Application.Abstractions;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Authentication;

public class PasswordService(IPasswordHasher<User> _passwordHasher) : IPasswordService
{
    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string enteredPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, enteredPassword);
        return result == PasswordVerificationResult.Success;
    }
}
