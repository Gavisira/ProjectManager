using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Contexts;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace ProjectManager.Infrastructure.SQLServer.Repositories;
[ExcludeFromCodeCoverage]
public class TaskHistoryRepository(ProjectManagerDbContext context)
    : BaseRepository<ProjectTaskHistory>(context), ITaskHistoryRepository
{
    public async Task<List<ProjectTaskHistory>> GetAllTaskHistoryByTaskId(int taskId)
    {
        return await _context.ProjectTaskHistories.Where(p => p.IsDeleted == false)
            .Where(x => x.ProjectTaskId == taskId).ToListAsync();
    }
}