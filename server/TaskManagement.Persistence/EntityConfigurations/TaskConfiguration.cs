using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;
using TaskManagement.Persistence.Constants;

namespace TaskManagement.Persistence.EntityConfigurations;

internal sealed class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable(TableNames.Tasks);

        builder.HasKey(t => t.Id);

        builder
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(t => t.TaggedUsers)
            .WithOne(tu => tu.Task)
            .HasForeignKey(tu => tu.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
