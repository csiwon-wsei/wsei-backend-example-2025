namespace ApplicationCore.Application.Commons;

public interface IGenericRepositoryAsync<T> where T: BaseIdentity
{
    Task<IQueryable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task<bool> DeleteByIdAsync(Guid id);
    Task SaveChangesAsync();
}