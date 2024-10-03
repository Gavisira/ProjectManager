using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Infrastructure.SQLServer.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        // Chave primária
        builder.HasKey(p => p.Id);

        // Propriedades
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Relacionamento um-para-muitos com ProjectTask
        builder.HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey("ProjectId");
    }
}