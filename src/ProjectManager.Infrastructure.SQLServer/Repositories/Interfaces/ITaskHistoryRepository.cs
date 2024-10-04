using ProjectManager.Domain.Entities;

namespace ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

public interface ITaskHistoryRepository : IBaseRepository<ProjectTaskHistory>
{
    public Task<List<ProjectTaskHistory>> GetAllTaskHistoryByTaskId(int taskId);
}