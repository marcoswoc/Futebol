using Futebol.Infrastructure.Models;
using System.Linq.Expressions;

namespace Futebol.Infrastructure.Interfaces;
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(Guid Id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task DeleteAsync(Guid Id);
    Task<IEnumerable<TEntity>> FindExpressionAsync(Expression<Func<TEntity, bool>> predicate);
}
