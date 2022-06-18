using FutebolApi.Models;
using FutebolApi.Models.Round;

namespace FutebolApi.Services.Interfaces;

public interface IRoundService
{
    Task<ResponseModel<IEnumerable<RoundModel>>> GetAllAsync();
    Task<ResponseModel<RoundModel>> GetByIdAsync(Guid id);
    Task<ResponseModel<RoundModel>> CreateAsync(CreateRoundModel model);
}
