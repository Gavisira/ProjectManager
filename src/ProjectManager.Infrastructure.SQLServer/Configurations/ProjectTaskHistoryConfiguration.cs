using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Infrastructure.SQLServer.Configurations;

public class ProjectTaskHistoryConfiguration : IEntityTypeConfiguration<ProjectTaskHistory>
{
    public void Configure(EntityTypeBuilder<ProjectTaskHistory> builder)
    {
        // Chave primária
        builder.HasKey(h => h.Id);

        // Propriedades
        builder.Property(h => h.HistoryDescription)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(h => h.ChangeDate)
            .IsRequired();

        // Relacionamento muitos-para-um com User
        builder.HasOne(h => h.User)
            .WithMany()
            .HasForeignKey("UserId");

        // Propriedade de sombra para ProjectTaskId
        builder.Property<int>("ProjectTaskId");

        // Relacionamento muitos-para-um com ProjectTask
        builder.HasOne<ProjectTask>()
            .WithMany(t => t.TaskHistories)
            .HasForeignKey("ProjectTaskId");
    }
}