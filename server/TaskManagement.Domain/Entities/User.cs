using TaskManagement.Domain.Primitives;

namespace TaskManagement.Domain.Entities;

public class User : AggregateRoot, IAuditableEntity
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
    public DateTimeOffset CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
}
