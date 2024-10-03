using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Infrastructure.SQLServer.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        // Chave primária
        builder.HasKey(t => t.Id);

        // Propriedades

        builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(t => t.Priority)
            .IsRequired()
            .HasConversion<string>(); // Armazenar enum como string

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.CreatedDate)
            .IsRequired();

        builder.Property(t => t.TargetDate)
            .IsRequired();

        builder.Property(t => t.EndDate);

        // Relacionamentos

        // Relacionamento um-para-muitos com ProjectTaskHistory
        builder.HasMany(t => t.TaskHistories)
            .WithOne(h => h.ProjectTask)
            .HasForeignKey(h => h.ProjectTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento um-para-muitos com ProjectTaskComment
        builder.HasMany(t => t.Comments)
            .WithOne(c => c.ProjectTask)
            .HasForeignKey(c => c.ProjectTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento muitos-para-um com Project
        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}