namespace TaskManagement.API.Models.TaskModels;

public record CreateTaskRequest(
    string Title,
    int? RemindBeforeDeadlineByMinutes,
    DateTimeOffset DueDate);

