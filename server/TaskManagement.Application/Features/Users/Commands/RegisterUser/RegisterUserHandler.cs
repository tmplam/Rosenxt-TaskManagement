using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserHandler(IUserRepository _userRepository, IUnitOfWork _unitOfWork) : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        
    }
}
