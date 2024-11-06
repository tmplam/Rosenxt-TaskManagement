using TaskManagement.Application.Dtos;

namespace TaskManagement.API.Models.TaskModels;

public record GetTasksResponse(List<TaskItemDto> Tasks);
