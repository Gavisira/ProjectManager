using System.Text.Json;
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
        builder.Property(t => t.Priority)
            .IsRequired()
            .HasConversion<string>(); // Armazenar enum como string

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>(); // Armazenar enum como string

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.CreatedDate)
            .IsRequired();

        builder.Property(t => t.TargetDate)
            .IsRequired();

        builder.Property(t => t.EndDate);

        // Conversão da lista de Comments para string JSON
        builder.Property(t => t.Comments)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<IEnumerable<string>>(v, (JsonSerializerOptions)null))
            .HasColumnType("nvarchar(max)");

        // Relacionamento um-para-muitos com ProjectTaskHistory
        builder.HasMany(t => t.TaskHistories)
            .WithOne()
            .HasForeignKey("ProjectTaskId");

        // Relacionamento muitos-para-um com Project
        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey("ProjectId");
    }
}