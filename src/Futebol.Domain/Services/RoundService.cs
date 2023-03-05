using AutoMapper;
using Futebol.Domain.Dto;
using Futebol.Domain.Dto.Round;
using Futebol.Domain.Entity;
using Futebol.Domain.Repositories;
using Futebol.Domain.Services.Interfaces;

namespace Futebol.Domain.Services;
public class RoundService : IRoundService
{
    private readonly IRoundRepository _repository;
    private readonly IMapper _mapper;

    public RoundService(IRoundRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ResponseDto<RoundDto>> CreateAsync(CreateRoundDto dto)
    {
        if (dto.StartDate.Date >= dto.EndDate.Date)
            return new() { Success = false, Message = "Data Inicio maior ou igual que Data Fim" };

        var exists = await _repository.FindExpressionAsync(x => x.EndDate.Date >= dto.StartDate.Date);
        if (exists.Any())
            return new() { Success = false, Message = "Já existe um Round Cadastrado para a data Inicio" };

        var entity = await _repository.CreateAsync(_mapper.Map<Round>(dto));
        return new() { Data = _mapper.Map<RoundDto>(entity), Success = true, Message = "Partida criada com sucesso!" };
    }

    public async Task<ResponseDto<IEnumerable<RoundDto>>> GetAllAsync()
    {
        var entity = await _repository.GetAllAsync();
        return new() { Data = _mapper.Map<IEnumerable<RoundDto>>(entity), Success = true };
    }

    public async Task<ResponseDto<RoundDto>> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return new() { Data = _mapper.Map<RoundDto>(entity), Success = true };
    }
}
