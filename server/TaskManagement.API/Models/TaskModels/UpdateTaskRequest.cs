namespace TaskManagement.API.Models.TaskModels;

public record UpdateTaskRequest(
    Guid Id,
    string Title,
    string Description,
    int? RemindBeforeDeadlineByMinutes,
    DateTimeOffset DueDate);