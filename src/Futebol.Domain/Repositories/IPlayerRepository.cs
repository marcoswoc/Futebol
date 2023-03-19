using Futebol.Domain.Entity;
using Futebol.Infrastructure.Interfaces;

namespace Futebol.Domain.Repositories;
public interface IPlayerRepository : IRepository<Player>
{
    Task<IEnumerable<Player>> GetAllPlayersWithUsersAsync();
}
