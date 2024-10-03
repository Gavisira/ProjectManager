using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Enums;

namespace ProjectManager.Infrastructure.SQLServer.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Chave primária
        builder.HasKey(u => u.Id);

        // Propriedades

        builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Document)
            .IsRequired()
            .HasMaxLength(20);

        var rolesComparer = new ValueComparer<List<EUserRole>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());

        builder.Property(u => u.Roles)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<EUserRole>>(v, (JsonSerializerOptions)null))
            .HasColumnType("nvarchar(max)")
            .Metadata.SetValueComparer(rolesComparer)
            ;

        // Relacionamento muitos-para-muitos com Project
        builder.HasMany(u => u.Projects)
            .WithMany(p => p.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserProject",
                j => j.HasOne<Project>().WithMany().HasForeignKey("ProjectId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("UserId", "ProjectId");
                    j.ToTable("UserProjects");
                });

        // Relacionamento um-para-muitos com ProjectTaskComment (se necessário)
        builder.HasMany<ProjectTaskComment>()
            .WithOne(c => c.CreatedUser)
            .HasForeignKey(c => c.CreatedUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}