using Futebol.Domain.Dto;
using Futebol.Domain.Dto.Round;

namespace Futebol.Domain.Services.Interfaces;
public interface IRoundService
{
    Task<ResponseDto<IEnumerable<RoundDto>>> GetAllAsync();
    Task<ResponseDto<RoundDto>> GetByIdAsync(Guid id);
    Task<ResponseDto<RoundDto>> CreateAsync(CreateRoundDto dto);
}