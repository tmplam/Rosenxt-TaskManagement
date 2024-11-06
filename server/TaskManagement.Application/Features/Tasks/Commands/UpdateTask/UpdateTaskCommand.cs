using FluentValidation;
using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Tasks.Commands.UpdateTask;

public record UpdateTaskCommand(
    Guid Id,
    string Title,
    int? RemindBeforeDeadlineByMinutes,
    DateTimeOffset DueDate) : ICommand<UpdateTaskResult>;

public record UpdateTaskResult(Guid Id);

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(task => task.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(task => task.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(task => task.DueDate).NotEmpty().WithMessage("DueDate is required");
    }
}