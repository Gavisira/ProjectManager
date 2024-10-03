using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Context;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Infrastructure.SQLServer.Repositories;

public class TaskRepository(ProjectManagerDbContext context) : BaseRepository<ProjectTask>(context),ITaskRepository
{
    public new async Task<ProjectTask> UpdateAsync(ProjectTask entity)
    {
        var task = await GetByIdAsNoTrackingAsync(entity.Id);
        entity.Priority = task.Priority;
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
}