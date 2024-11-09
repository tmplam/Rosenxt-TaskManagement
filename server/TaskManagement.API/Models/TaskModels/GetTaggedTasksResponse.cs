using TaskManagement.Application.Dtos;

namespace TaskManagement.API.Models.TaskModels;

public record GetTaggedTasksResponse(List<TaskItemDto> TaggedTasks);
