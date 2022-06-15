namespace FutebolApi.Data;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(Guid Id);
    Task<IEnumerable<TEntity>> GetAllAsync();    
    Task DeleteAsync(Guid Id);
}
