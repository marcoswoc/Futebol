using Futebol.Application.Models;
using Futebol.Application.Models.Player;
using Microsoft.AspNetCore.Http;

namespace Futebol.Application.Applications.Interfaces;
public interface IPlayerApplication
{
    Task<ResponseModel<PlayerModel>> UpdateAsync(UpdatePlayerModel model, Guid id);
    Task<ResponseModel<PlayerModel>> GetByIdAsync(Guid id);
    Task<ResponseModel<IEnumerable<PlayerModel>>> GetAllAsync();
    Task<ResponseModel<string>> UploadImageAsync(IFormFile model);
}
