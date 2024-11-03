using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Exceptions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserHandler(
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork,
    IPasswordService _passwordService)
    : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser is not null)
        {
            throw new BadRequestException($"Email \"{request.Email}\" already existed");
        }

        var user = User.Create(Guid.NewGuid(), request.Email, request.Name, request.Password);
        user.Password = _passwordService.HashPassword(user, user.Password);
        user = await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterUserResult(user.Id);
    }
}
