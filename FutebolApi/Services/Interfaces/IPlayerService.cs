using FutebolApi.Entity;
using FutebolApi.Models.Player;

namespace FutebolApi.Services.Interfaces;

public interface IPlayerService
{
    //Task<PlayerModel> CreateAsync(Player entity);
    Task<PlayerModel> UpdateAsync(UpdatePlayerModel model, Guid id);
    Task<PlayerModel> GetByIdAsync(Guid id);
    Task<IEnumerable<PlayerModel>> GetAllAsync();
    //Task DeleteAsync(Guid Id);
}
