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

    public async Task<IEnumerable<ProjectTask?>> GetAllTasksByUser(int userId)
    {
        try
        {
            var userProjects = _context.Users.Where(p => p.IsDeleted == false).Include(x => x.Projects).Where(p => p.IsDeleted == false).FirstOrDefault(x => x.Id == userId);
            var projects = await _context.Projects.Where(p => p.IsDeleted == false).Include(p => p.Tasks).Where(p => p.IsDeleted == false).Where(p => userProjects.Projects.Contains(p))
                .Select(p => p.Tasks.Where(p => p.IsDeleted == false)).ToListAsync();
            return projects.SelectMany(x => x);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}