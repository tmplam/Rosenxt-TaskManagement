namespace TaskManagement.Application.Dtos;

public record TaskItemDto(
    Guid Id,
    string Title,
    bool IsCompleted,
    int? RemindBeforeDeadlineByMinutes,
    DateTimeOffset DueDate,
    DateTimeOffset? ModifiedAt);