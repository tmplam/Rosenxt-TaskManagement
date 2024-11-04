using FluentValidation;
using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Tasks.Commands.CreateTask;

public record CreateTaskCommand(
    string Title, 
    string Description, 
    int? RemindBeforeDeadlineByMinutes, 
    DateTimeOffset DueDate) : ICommand<CreateTaskResult>;

public record CreateTaskResult(Guid Id);

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(task => task.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(task => task.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(task => task.DueDate).NotEmpty().WithMessage("DueDate is required");
    }
}