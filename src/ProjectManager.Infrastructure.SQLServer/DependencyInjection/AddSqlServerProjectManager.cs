using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Infrastructure.SQLServer.Contexts;
using ProjectManager.Infrastructure.SQLServer.Repositories;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace ProjectManager.Infrastructure.SQLServer.DependencyInjection;
[ExcludeFromCodeCoverage]
public static class AddSqlServerProjectManager
{
    public static IServiceCollection AddSqlServerProjectManagerSetup(this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ProjectManagerDbContext>(options => { options.UseSqlServer(connectionString); });
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ICommentTaskRepository, CommentTaskRepository>();
        services.AddScoped<ITaskHistoryRepository, TaskHistoryRepository>();
        return services;
    }
}