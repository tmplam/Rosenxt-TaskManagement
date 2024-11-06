namespace TaskManagement.API.Models.TaskModels;

public record UpdateTaskRequest(
    Guid Id,
    string Title,
    int? RemindBeforeDeadlineByMinutes,
    DateTimeOffset DueDate);