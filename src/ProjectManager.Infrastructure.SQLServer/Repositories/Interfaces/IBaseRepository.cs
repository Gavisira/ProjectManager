namespace ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    public Task<IEnumerable<T?>> GetAllAsync();
    public Task<T?> GetByIdAsync(int id);
    public Task<T> AddAsync(T entity);
    public Task<T?> UpdateAsync(T entity);
    public Task<bool> DeleteAsync(int id);
    public Task<T?> GetByIdAsNoTrackingAsync(int id);
    public Task<IEnumerable<T>> GetAllAsNoTrackingAsync();
}