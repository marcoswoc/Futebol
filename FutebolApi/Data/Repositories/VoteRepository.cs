using FutebolApi.Data.Repositories.Interfaces;
using FutebolApi.Entity;

namespace FutebolApi.Data.Repositories;

public class VoteRepository<TContext> : Repository<Vote>, IVoteRepository
    where TContext : DataContext
{
    public VoteRepository(DataContext context) : base(context)
    {
    }
}
