using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Tasks.Queries.GetTasks;

public class GetTasksHandler : IQueryHandler<GetTasksQuery, GetTasksResult>
{
    public Task<GetTasksResult> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
