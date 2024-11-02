using TaskManagement.Domain.Primitives;

namespace TaskManagement.Domain.Entities;

public class User : AggregateRoot, IAuditableEntity
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    public ICollection<TaskUserTag> TaggedTasks { get; set; } = new List<TaskUserTag>();

    public DateTimeOffset CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
}
