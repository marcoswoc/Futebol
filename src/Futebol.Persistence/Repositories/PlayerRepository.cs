using Futebol.Domain.Entity;
using Futebol.Domain.Repositories;
using Futebol.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Futebol.Persistence.Repositories;
public class PlayerRepository<TContext> : RepositoryBase<Player>, IPlayerRepository
    where TContext : DbContext
{
    public PlayerRepository(TContext context) : base(context)
    {
    }
}
