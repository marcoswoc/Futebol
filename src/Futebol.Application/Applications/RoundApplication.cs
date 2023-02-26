using AutoMapper;
using Futebol.Application.Applications.Interfaces;
using Futebol.Domain.Dto.Round;
using Futebol.Domain.Services.Interfaces;
using Futebol.Shared.Models;
using Futebol.Shared.Models.Round;

namespace Futebol.Application.Applications;
public class RoundApplication : IRoundApplication
{
    private readonly IRoundService _roundService;
    private readonly IMapper _mapper;

    public RoundApplication(IRoundService roundService, IMapper mapper)
    {
        _roundService = roundService;
        _mapper = mapper;
    }

    public async Task<ResponseModel<RoundModel>> CreateAsync(CreateRoundModel model)
    {
        var dto = await _roundService.CreateAsync(_mapper.Map<CreateRoundDto>(model));
        return _mapper.Map<ResponseModel<RoundModel>>(dto);
    }

    public async Task<ResponseModel<IEnumerable<RoundModel>>> GetAllAsync()
    {
        var dtos = await _roundService.GetAllAsync();
        return _mapper.Map<ResponseModel<IEnumerable<RoundModel>>>(dtos);
    }

    public async Task<ResponseModel<RoundModel>> GetByIdAsync(Guid id)
    {
        var dto = await _roundService.GetByIdAsync(id);
        return _mapper.Map<ResponseModel<RoundModel>>(dto);
    }
}
