using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain;
using ProjectManager.Infrastructure.SQLServer.Context;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Infrastructure.SQLServer.Repositories;

public class BaseRepository<T>(ProjectManagerDbContext context) : IBaseRepository<T>
    where T : BaseEntity, new()
{

    public readonly ProjectManagerDbContext _context = context;

    public async Task<IEnumerable<T?>> GetAllAsync()
    {
        return _context.Set<T>().AsEnumerable();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        try
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.IsDeleted = true;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<T?> GetByIdAsNoTrackingAsync(int id) => await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id)??new T();

    public async Task<IEnumerable<T>> GetAllAsNoTrackingAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }
}