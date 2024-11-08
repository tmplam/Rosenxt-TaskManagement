using TaskManagement.Domain.DomainEvents;
using TaskManagement.Domain.Primitives;

namespace TaskManagement.Domain.Entities;

public class User : AggregateRoot, IAuditableEntity
{
    private readonly List<TaskItem> _tasks = new();
    private readonly List<TaskUserTag> _taggedTasks = new();

    private User(Guid id, string email, string name, string password) : base(id)
    {
        Email = email;
        Name = name;
        Password = password;
    }

    private User()
    {
    }

    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public IReadOnlyCollection<TaskItem> Tasks => _tasks;
    public IReadOnlyCollection<TaskUserTag> TaggedTasks => _taggedTasks;

    public DateTimeOffset CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }

    public static User Create(Guid id, string email, string name, string password)
    {
        var user = new User(id, email, name, password);
        return user;
    }

    public void AddNewTask(TaskItem task)
    {
        _tasks.Add(task);
    }
}
