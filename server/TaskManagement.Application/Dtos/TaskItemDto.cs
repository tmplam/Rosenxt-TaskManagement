namespace TaskManagement.Application.Dtos;

public record TaskItemDto(
    Guid Id,
    string Title,
    bool IsCompleted,
    int? RemindBeforeDeadlineByMinutes,
    UserDto User,
    DateTimeOffset DueDate,
    DateTimeOffset? ModifiedAt);