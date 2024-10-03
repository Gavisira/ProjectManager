using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;
using System.Reflection;
using System;

namespace ProjectManager.Infrastructure.SQLServer.Context;

public class ProjectManagerDbContext(DbContextOptions<ProjectManagerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<ProjectTaskComment> ProjectTaskComments { get; set; }
    public DbSet<ProjectTaskHistory> ProjectTaskHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}