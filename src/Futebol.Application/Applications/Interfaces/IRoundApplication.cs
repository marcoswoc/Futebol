using Futebol.Application.Models;
using Futebol.Application.Models.Round;

namespace Futebol.Application.Applications.Interfaces;
public interface IRoundApplication
{
    Task<ResponseModel<IEnumerable<RoundModel>>> GetAllAsync();
    Task<ResponseModel<RoundModel>> GetByIdAsync(Guid id);
    Task<ResponseModel<RoundModel>> CreateAsync(CreateRoundModel model);
}
