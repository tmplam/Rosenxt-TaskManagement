namespace TaskManagement.API.Models.TaskModels;

public record TagUsersToTaskRequest(Guid TaskId, List<string> Emails);