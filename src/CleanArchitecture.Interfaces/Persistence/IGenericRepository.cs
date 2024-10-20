namespace CleanArchitecture.Interfaces.Persistence;

public interface IGenericRepository<T> where T : class
{
    /* Commands */
    Task<bool> InsertAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);

    /* Queries */
    Task<T?> GetAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync(object? obj = null);
    Task<int> CountAsync();
    Task<IEnumerable<T>> GetPaginationAsync(int pageNumber, int pageSize, object? obj = null);
}