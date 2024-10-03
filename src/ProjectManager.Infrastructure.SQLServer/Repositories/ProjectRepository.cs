using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Contexts;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Infrastructure.SQLServer.Repositories;

public class ProjectRepository(ProjectManagerDbContext context) : BaseRepository<Project>(context), IProjectRepository
{
    public new async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects.Where(p => p.IsDeleted == false).Include(x => x.Tasks)
            .Where(p => p.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
    }

    public new async Task<Project?> GetByIdAsNoTrackingAsync(int id)
    {
        return await _context.Projects.Where(p => p.IsDeleted == false).AsNoTracking().Include(x => x.Tasks)
            .Where(p => p.IsDeleted == false).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Project>> GetAllProjectsFromUserAsync(int userId)
    {
        return await _context.Projects.Where(p => p.IsDeleted == false).Include(x => x.Users)
            .Where(p => p.IsDeleted == false).Where(x => x.Users.Any(u => u.Id == userId && u.IsDeleted == false))
            .ToListAsync();
    }

    public async Task<bool> AddUserToProject(int userId, int projectId)
    {
        var project = await _context.Projects.Where(p => p.IsDeleted == false).Include(x => x.Users)
            .Where(p => p.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == projectId);
        if (project == null) return false;
        var user = await _context.Users.Where(p => p.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) return false;

        var projectUsers = project.Users.ToList();
        projectUsers.Add(user);
        project.Users = projectUsers;

        _context.Entry(project).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public new async Task<bool> DeleteAsync(int id)
    {
        var tasks = await _context.Set<ProjectTask>().Where(p => p.IsDeleted == false).Where(x => x.Project.Id == id)
            .ToListAsync();
        if (!tasks.Any())
            try
            {
                var entity = await _context.Projects.Where(p => p.IsDeleted == false)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (entity == null) return false;

                _context.Projects.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        return false;
    }
}