using Futebol.Domain.Entity;
using Futebol.Domain.Repositories;
using Futebol.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Futebol.Persistence.Repositories;
public class RoundRepository<TContext> : RepositoryBase<Round>, IRoundRepository
    where TContext : DbContext
{
    public RoundRepository(TContext context) : base(context)
    {
    }
}
