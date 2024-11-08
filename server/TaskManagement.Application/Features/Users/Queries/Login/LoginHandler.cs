using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Features.Users.Queries.Login;

public class LoginHandler(
    IUserRepository _userRepository,
    IPasswordService _passwordService,
    IJwtProvider _jwtProvider) 
    : IQueryHandler<LoginQuery, LoginResult>
{
    public async Task<LoginResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(query.Email);
        if (user is null)
        {
            throw new BadRequestException($"Account with email \"{query.Email}\" not existed");
        }

        if (_passwordService.VerifyPassword(user, query.Password))
        {
            var token = _jwtProvider.GenerateToken(user);
            return new LoginResult(token);
        }    

        throw new BadRequestException($"Password not match");
    }
}
