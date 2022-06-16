using FutebolApi.Data.Repositories.Interfaces;
using FutebolApi.Entity;

namespace FutebolApi.Data.Repositories;

public class PlayerRepository<TContext> : Repository<Player>, IPlayerRepository
    where TContext : DataContext
{
    public PlayerRepository(DataContext context) : base(context)
    {
    }
}
