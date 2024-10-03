using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Context;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Infrastructure.SQLServer.Repositories;

public class TaskHistoryRepository(ProjectManagerDbContext context) : BaseRepository<ProjectTaskHistory>(context), ITaskHistoryRepository
{
    
}