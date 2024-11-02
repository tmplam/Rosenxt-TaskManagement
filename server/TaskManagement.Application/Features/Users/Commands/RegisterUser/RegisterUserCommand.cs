using FluentValidation;
using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Users.Commands.RegisterUser;

public record RegisterUserCommand(string Email, string Name, string Password) : ICommand<RegisterUserResult>;
public record RegisterUserResult(Guid Id);

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Must be a valid email");
        RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required");
    }
}