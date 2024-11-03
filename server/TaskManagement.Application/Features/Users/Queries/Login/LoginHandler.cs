using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Exceptions;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Users.Queries.Login;

public class LoginHandler(
    IUserRepository _userRepository,
    IPasswordService _passwordService,
    IJwtProvider _jwtProvider) 
    : IQueryHandler<LoginQuery, LoginResult>
{
    public async Task<LoginResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            throw new BadRequestException($"Account with email \"{request.Email}\" not existed");
        }

        if (_passwordService.VerifyPassword(user, request.Password))
        {
            var token = _jwtProvider.GenerateToken(user);
            return new LoginResult(token);
        }    

        throw new BadRequestException($"Password not match");
    }
}
