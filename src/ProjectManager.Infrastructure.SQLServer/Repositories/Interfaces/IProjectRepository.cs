using ProjectManager.Domain.Entities;

namespace ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<IEnumerable<Project>> GetAllProjectsFromUserAsync(int userId);
    Task<bool> AddUserToProject(int userId, int projectId);
}