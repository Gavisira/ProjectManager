using ProjectManager.Domain.Entities;

namespace ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

public interface ITaskRepository : IBaseRepository<ProjectTask>
{
    public Task<IEnumerable<ProjectTask?>> GetAllTasksByUser(int userId);
}