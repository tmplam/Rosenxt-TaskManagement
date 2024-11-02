using TaskManagement.Domain.Primitives;

namespace TaskManagement.Domain.Entities;

public class TaskUserTag : Entity, IAuditableEntity
{
    public Guid TaskId { get; set; }
    public TaskItem Task { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
}
