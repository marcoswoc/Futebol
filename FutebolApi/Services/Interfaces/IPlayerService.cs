using FutebolApi.Models;
using FutebolApi.Models.Player;

namespace FutebolApi.Services.Interfaces;

public interface IPlayerService
{
    Task<ResponseModel<PlayerModel>> UpdateAsync(UpdatePlayerModel model, Guid id);
    Task<ResponseModel<PlayerModel>> GetByIdAsync(Guid id);
    Task<ResponseModel<IEnumerable<PlayerModel>>> GetAllAsync();
}
