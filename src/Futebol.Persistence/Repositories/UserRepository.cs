using Futebol.Domain.Entity;
using Futebol.Domain.Repositories;
using Futebol.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Futebol.Persistence.Repositories;
public class UserRepository<TContext> : RepositoryBase<User>, IUserRepository
    where TContext : DbContext
{
    public UserRepository(TContext context) : base(context)
    {
    }
}
