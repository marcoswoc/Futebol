using FutebolApi.Models;
using FutebolApi.Models.Vote;

namespace FutebolApi.Services.Interfaces;

public interface IVoteService
{
    Task<ResponseModel<IEnumerable<VoteModel>>> GetAllAsync();    
    Task<ResponseModel<VoteModel>> CreateAsync(CreateVoteModel model);
    Task<ResponseModel<VoteModel>> UpdateAsync(UpdateVoteModel model, Guid id);
}
