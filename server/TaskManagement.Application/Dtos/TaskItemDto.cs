namespace TaskManagement.Application.Dtos;

public record TaskItemDto(
    string Title,
    string Description,
    bool IsCompleted,
    int? RemindBeforeDeadlineByMinutes,
    DateTimeOffset DueDate,
    DateTimeOffset? ModifiedAt);