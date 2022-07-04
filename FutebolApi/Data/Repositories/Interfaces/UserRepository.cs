using FutebolApi.Entity;

namespace FutebolApi.Data.Repositories.Interfaces;

public class UserRepository<TContext> : Repository<User>, IUserRepository
    where TContext : DataContext
{
    public UserRepository(DataContext context) : base(context)
    {
    }
}
