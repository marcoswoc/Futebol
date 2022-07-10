using Futebol.Domain.Entity;
using Futebol.Domain.Repositories;
using Futebol.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Futebol.Persistence.Repositories;
public class AuditReposiotry<TContext> : RepositoryBase<Audit>, IAuditRepository
    where TContext : DbContext
{
    public AuditReposiotry(TContext context) : base(context)
    {
    }
}
