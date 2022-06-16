using AutoMapper;
using FutebolApi.Data.Repositories.Interfaces;
using FutebolApi.Models.Player;
using FutebolApi.Services.Interfaces;

namespace FutebolApi.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _repository;
    private readonly IMapper _mapper;

    public PlayerService(IPlayerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlayerModel>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<PlayerModel>>(entities);
    }

    public async Task<PlayerModel> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<PlayerModel>(entity);
    }

    public async Task<PlayerModel> UpdateAsync(UpdatePlayerModel model, Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        entity = _mapper.Map(model, entity);

        await _repository.UpdateAsync(entity);

        return _mapper.Map<PlayerModel>(entity);
    }
}
