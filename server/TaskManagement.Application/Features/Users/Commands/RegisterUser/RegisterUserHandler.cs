using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Exceptions;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserHandler(
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork,
    IPasswordService _passwordService)
    : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(command.Email);
        if (existingUser is not null)
        {
            throw new BadRequestException($"Email \"{command.Email}\" already existed");
        }

        var user = User.Create(Guid.NewGuid(), command.Email, command.Name, command.Password);
        user.Password = _passwordService.HashPassword(user, user.Password);
        user = await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterUserResult(user.Id);
    }
}
