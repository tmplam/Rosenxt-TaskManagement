using TaskManagement.Application.Dtos;

namespace TaskManagement.API.Models.TaskModels;

public record GetTaskByIdResponse(TaskItemDto Task);