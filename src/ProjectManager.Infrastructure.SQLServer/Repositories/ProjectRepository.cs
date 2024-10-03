using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Context;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Infrastructure.SQLServer.Repositories;

public class ProjectRepository(ProjectManagerDbContext context) : BaseRepository<Project>(context), IProjectRepository
{
    public new async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects.Where(p => p.IsDeleted == false).Include(x => x.Tasks).Where(p => p.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
    }

    public new async Task<Project?> GetByIdAsNoTrackingAsync(int id)
    {
        return await _context.Projects.Where(p => p.IsDeleted == false).AsNoTracking().Include(x => x.Tasks).Where(p => p.IsDeleted == false).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
    public new async Task<bool> DeleteAsync(int id)
    {
        var tasks = await _context.Set<ProjectTask>().Where(p => p.IsDeleted == false).Where(x => x.Project.Id == id).ToListAsync();
        if (!tasks.Any())
        {
            try
            {
                var entity = await _context.Projects.Where(p => p.IsDeleted == false).FirstOrDefaultAsync(p=>p.Id==id);
                if (entity == null)
                {
                    return false;
                }

                _context.Projects.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        return false;
    }
}