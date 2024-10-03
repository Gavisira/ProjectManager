﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
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