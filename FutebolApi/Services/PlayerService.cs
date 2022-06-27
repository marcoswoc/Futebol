using AutoMapper;
using FutebolApi.Data.Repositories.Interfaces;
using FutebolApi.Models;
using FutebolApi.Models.Player;
using FutebolApi.Services.Interfaces;

namespace FutebolApi.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _repository;
    private readonly IVoteRepository _voteRepository;
    private readonly IMapper _mapper;

    public PlayerService(IPlayerRepository repository, IMapper mapper, IVoteRepository voteRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _voteRepository = voteRepository;
    }

    public async Task<ResponseModel<IEnumerable<PlayerModel>>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return new() { Data = _mapper.Map<IEnumerable<PlayerModel>>(entities) };
    }  

    public async Task<ResponseModel<PlayerModel>> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return new() { Data = _mapper.Map<PlayerModel>(entity) };
    }

    public async Task<ResponseModel<PlayerModel>> UpdateAsync(UpdatePlayerModel model, Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        entity = _mapper.Map(model, entity);

        await _repository.UpdateAsync(entity);

        return new() { Data = _mapper.Map<PlayerModel>(entity) };
    }
}
