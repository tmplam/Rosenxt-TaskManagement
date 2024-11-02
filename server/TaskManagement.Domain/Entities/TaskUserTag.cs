using TaskManagement.Domain.Primitives;

namespace TaskManagement.Domain.Entities;

public class TaskUserTag : Entity, IAuditableEntity
{
    public int TaskId { get; set; }
    public Task Task { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
}
