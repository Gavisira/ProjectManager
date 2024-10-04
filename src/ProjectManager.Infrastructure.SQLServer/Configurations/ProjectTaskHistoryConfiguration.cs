using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace ProjectManager.Infrastructure.SQLServer.Configurations;
[ExcludeFromCodeCoverage]
public class ProjectTaskHistoryConfiguration : IEntityTypeConfiguration<ProjectTaskHistory>
{
    public void Configure(EntityTypeBuilder<ProjectTaskHistory> builder)
    {
        // Chave primária
        builder.HasKey(h => h.Id);

        // Propriedades

        builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false);
        builder.Property(h => h.HistoryDescription)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(h => h.ChangeDate)
            .IsRequired();

        // Relacionamentos

        // Relacionamento muitos-para-um com User
        builder.HasOne(h => h.User)
            .WithMany()
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento muitos-para-um com ProjectTask
        builder.HasOne(h => h.ProjectTask)
            .WithMany(t => t.TaskHistories)
            .HasForeignKey(h => h.ProjectTaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}