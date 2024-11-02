using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;
using TaskManagement.Persistence.Constants;

namespace TaskManagement.Persistence.EntityConfigurations;

internal sealed class TaskUserTagConfiguration : IEntityTypeConfiguration<TaskUserTag>
{
    public void Configure(EntityTypeBuilder<TaskUserTag> builder)
    {
        builder.ToTable(TableNames.TaskUserTags);
    }
}
