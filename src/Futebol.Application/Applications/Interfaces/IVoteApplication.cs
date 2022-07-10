using Futebol.Application.Models;
using Futebol.Application.Models.Vote;

namespace Futebol.Application.Applications.Interfaces;
public interface IVoteApplication
{
    Task<ResponseModel<IEnumerable<VoteModel>>> GetAllAsync();
    Task<ResponseModel<VoteModel>> CreateAsync(CreateVoteModel model);
    Task<ResponseModel<VoteModel>> UpdateAsync(UpdateVoteModel model, Guid id);
    Task<ResponseModel<IEnumerable<VoteAvarageModel>>> GetAllAverageAsync();
}
