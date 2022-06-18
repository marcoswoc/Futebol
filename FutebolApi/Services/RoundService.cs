using AutoMapper;
using FutebolApi.Data.Repositories.Interfaces;
using FutebolApi.Entity;
using FutebolApi.Models;
using FutebolApi.Models.Round;
using FutebolApi.Services.Interfaces;

namespace FutebolApi.Services;

public class RoundService : IRoundService
{
    private readonly IRoundRepository _repository;
    private readonly IMapper _mapper;

    public RoundService(IRoundRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseModel<RoundModel>> CreateAsync(CreateRoundModel model)
    {
        var entity = await _repository.CreateAsync(_mapper.Map<Round>(model));

        return new() { Data = _mapper.Map<RoundModel>(entity) };
    }

    public async Task<ResponseModel<IEnumerable<RoundModel>>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return new() { Data = _mapper.Map<IEnumerable<RoundModel>>(entities) };
    }

    public async Task<ResponseModel<RoundModel>> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        return new() { Data = _mapper.Map<RoundModel>(entity) };
    }

}
