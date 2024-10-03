using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Infrastructure.SQLServer.Configurations;

public class ProjectTaskCommentConfiguration : IEntityTypeConfiguration<ProjectTaskComment>
{
    public void Configure(EntityTypeBuilder<ProjectTaskComment> builder)
    {
        builder.HasKey(c => c.Id);

        // Propriedades
        builder.Property(c => c.Comment)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.CreatedDate)
            .IsRequired();

        // Relacionamentos

        // Relacionamento muitos-para-um com ProjectTask
        builder.HasOne(c => c.ProjectTask)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.ProjectTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento muitos-para-um com User
        builder.HasOne(c => c.CreatedUser)
            .WithMany()
            .HasForeignKey(c => c.CreatedUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}