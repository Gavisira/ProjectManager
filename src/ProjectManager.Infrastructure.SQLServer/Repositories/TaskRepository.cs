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
            var userProjects = _context.Users.Include(x => x.Projects).FirstOrDefault(x => x.Id == userId);
            var projects = await _context.Projects.Include(p => p.Tasks).Where(p => userProjects.Projects.Contains(p))
                .Select(p => p.Tasks).ToListAsync();
            return projects.SelectMany(x => x);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}