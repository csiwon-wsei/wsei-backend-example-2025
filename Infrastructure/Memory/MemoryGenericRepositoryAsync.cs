using ApplicationCore.Application.Commons;

namespace Infrastructure.Memory;

public class MemoryGenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : BaseIdentity
{
    private readonly Dictionary<Guid, T> _dic = new Dictionary<Guid, T>();   
    public async Task<IQueryable<T>> GetAllAsync()
    {
        await Task.Delay(1);
        return _dic.Values.AsQueryable();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        await Task.Delay(1);
        return _dic.ContainsKey(id) ? _dic[id] : default(T);
    }

    public async Task<T> AddAsync(T entity)
    {
        await Task.Delay(1);
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        _dic.Add(entity.Id, entity);
        return entity;
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        await Task.Delay(1);
        if (!_dic.ContainsKey(entity.Id))
        {
            return null;
        }
        return _dic[entity.Id] = entity;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        await Task.Delay(1);
        return _dic.Remove(id);
    }

    public async Task SaveChangesAsync()
    {
        await Task.Delay(1);
    }
}