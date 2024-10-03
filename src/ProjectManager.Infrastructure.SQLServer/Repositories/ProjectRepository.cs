using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Context;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Infrastructure.SQLServer.Repositories;

public class ProjectRepository(ProjectManagerDbContext context) : BaseRepository<Project>(context), IProjectRepository
{
    public new async Task<bool> DeleteAsync(int id)
    {
        var tasks = await _context.Set<ProjectTask>().Where(x => x.Project.Id == id).ToListAsync();
        if (!tasks.Any())
        {
            try
            {
                var entity = await _context.Projects.FindAsync(id);
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