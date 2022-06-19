using AutoMapper;
using FutebolApi.Data.Repositories.Interfaces;
using FutebolApi.Entity;
using FutebolApi.Models;
using FutebolApi.Models.Vote;
using FutebolApi.Services.Interfaces;

namespace FutebolApi.Services;

public class VoteService : IVoteService
{
    private readonly IVoteRepository _repository;
    private readonly IMapper _mapper;

    public VoteService(IVoteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async  Task<ResponseModel<VoteModel>> CreateAsync(CreateVoteModel model)
    {
        var entity = await _repository.CreateAsync(_mapper.Map<Vote>(model));

        return new() { Data = _mapper.Map<VoteModel>(entity) };
    }

    public async Task<ResponseModel<IEnumerable<VoteModel>>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return new() { Data = _mapper.Map<IEnumerable<VoteModel>>(entities) };
    }

    public async  Task<ResponseModel<VoteModel>> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        return new() { Data = _mapper.Map<VoteModel>(entity) };
    }
}
