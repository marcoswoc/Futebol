using FutebolApi.Data.Repositories.Interfaces;
using FutebolApi.Entity;

namespace FutebolApi.Data.Repositories;

public class RoundRepository<TContext> : Repository<Round>, IRoundRepository
    where TContext : DataContext
{
    public RoundRepository(DataContext context) : base(context)
    {
    }
}
