using Futebol.Domain.Dto;
using Futebol.Domain.Dto.Vote;

namespace Futebol.Domain.Services.Interfaces;
public interface IVoteService
{
    Task<ResponseDto<IEnumerable<VoteDto>>> GetAllAsync(string userEmail);
    Task<ResponseDto<VoteDto>> CreateAsync(CreateVoteDto dto, string userEmail);
    Task<ResponseDto<VoteDto>> UpdateAsync(UpdateVoteDto dto, Guid id);
    Task<ResponseDto<IEnumerable<VoteAvarageDto>>> GetAllAverageAsync();
}