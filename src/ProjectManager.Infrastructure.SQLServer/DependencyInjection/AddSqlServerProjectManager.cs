using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Infrastructure.SQLServer.Context;
using ProjectManager.Infrastructure.SQLServer.Repositories;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Infrastructure.SQLServer.DependencyInjection;

public static class AddSqlServerProjectManager
{
    public static IServiceCollection AddSqlServerProjectManagerSetup(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ProjectManagerDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        return services;
    }
}