using FutebolApi.Models;
using FutebolApi.Models.Vote;

namespace FutebolApi.Services.Interfaces;

public interface IVoteService
{
    Task<ResponseModel<IEnumerable<VoteModel>>> GetAllAsync();
    Task<ResponseModel<VoteModel>> GetByIdAsync(Guid id);
    Task<ResponseModel<VoteModel>> CreateAsync(CreateVoteModel model);
}
