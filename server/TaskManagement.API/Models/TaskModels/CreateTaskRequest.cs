namespace TaskManagement.API.Models.TaskModels;

public record CreateTaskRequest(
    string Title,
    string Description,
    int? RemindBeforeDeadlineByMinutes,
    DateTimeOffset DueDate);

