using Futebol.Infrastructure.Interfaces;
using Futebol.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Futebol.Persistence.Repositories.Base;
public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly DbContext _context;

    public RepositoryBase(DbContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Add(entity);

        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public virtual async Task DeleteAsync(Guid Id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(Id);

        if (entity is not null)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task<IEnumerable<TEntity>> FindExpressionAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid Id)
    {
        return await _context.Set<TEntity>().FindAsync(Id);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
}
