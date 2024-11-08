using TaskManagement.Domain.DomainEvents;
using TaskManagement.Domain.Primitives;

namespace TaskManagement.Domain.Entities;

public class TaskUserTag : AggregateRoot, IAuditableEntity
{
    private TaskUserTag(Guid id, Guid taskId, Guid userId) : base(id)
    {
        TaskId = taskId;
        UserId = userId;
    }

    public Guid TaskId { get; set; }
    public TaskItem? Task { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }

    public static TaskUserTag Create(Guid id, Guid taskId, Guid userId)
    {
        var tag = new TaskUserTag(id, taskId, userId);
        tag.RaiseDomainEvent(new TagUserToTaskDomainEvent(Guid.NewGuid(), userId, taskId));
        return tag;
    }
}
