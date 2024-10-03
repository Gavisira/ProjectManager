using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Enums;
using System.Text.Json;

namespace ProjectManager.Infrastructure.SQLServer.Configurations;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Document)
            .IsRequired()
            .HasMaxLength(20);

        // Conversão da lista de Roles para string JSON
        builder.Property(u => u.Roles)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<EUserRole>>(v, (JsonSerializerOptions)null))
            .HasColumnType("nvarchar(max)");

        // Relacionamento muitos-para-muitos com Project
        builder.HasMany(u => u.Projects)
            .WithMany(p => p.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserProject",
                j => j.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j =>
                {
                    j.HasKey("UserId", "ProjectId");
                    j.ToTable("UserProjects");
                });
    }
}