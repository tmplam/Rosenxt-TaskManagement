using TaskManagement.Domain.Primitives;

namespace TaskManagement.Domain.Entities;

public class TaskItem : Entity, IAuditableEntity
{
    private TaskItem(
        Guid id, 
        string title, 
        string description, 
        bool isCompleted, 
        bool isNotified, 
        int remindBeforeDeadlineByMinutes,
        DateTimeOffset dueDate,
        Guid userId) : base(id)
    {
        Title = title;
        Description = description;
        IsCompleted = isCompleted;
        IsNotified = isNotified;
        RemindBeforeDeadlineByMinutes = remindBeforeDeadlineByMinutes;
        DueDate = dueDate;
        UserId = userId;

    }
    private TaskItem() 
    { 
    }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
    public bool IsNotified { get; set; } = false;
    public int? RemindBeforeDeadlineByMinutes { get; set; }
    public DateTimeOffset DueDate { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }
    public ICollection<TaskUserTag> TaggedUsers { get; set; } = new List<TaskUserTag>();

    public DateTimeOffset CreatedAt { get; set; }
    public Guid? CreatedBy { get; set ; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }

    public static TaskItem Create(
        Guid id,
        string title,
        string description,
        int remindBeforeDeadlineByMinutes,
        DateTimeOffset dueDate,
        Guid userId)
    {
        var task = new TaskItem(id, title, description, false, false, remindBeforeDeadlineByMinutes, dueDate, userId);
        return task;
    }
}
