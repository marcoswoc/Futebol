using Futebol.Domain.Entity;
using Futebol.Domain.Repositories;
using Futebol.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Futebol.Persistence.Repositories;
public class VoteRepository<TContext> : RepositoryBase<Vote>, IVoteRepository
    where TContext : DbContext
{
    public VoteRepository(TContext context) : base(context)
    {
    }
}
