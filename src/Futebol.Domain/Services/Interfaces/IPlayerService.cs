using Futebol.Domain.Dto;
using Futebol.Domain.Dto.Player;
using Microsoft.AspNetCore.Http;

namespace Futebol.Domain.Services.Interfaces;
public interface IPlayerService
{
    Task<ResponseDto<PlayerDto>> UpdateAsync(UpdatePlayerDto dto, Guid id);
    Task<ResponseDto<PlayerDto>> GetByIdAsync(Guid id);
    Task<ResponseDto<IEnumerable<PlayerDto>>> GetAllAsync();
    Task<ResponseDto<string>> UploadImageAsync(IFormFile model, string userEmail);
}
