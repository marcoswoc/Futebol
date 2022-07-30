using AutoMapper;
using Futebol.Application.Applications.Interfaces;
using Futebol.Application.Models;
using Futebol.Application.Models.Player;
using Futebol.Domain.Dto.Player;
using Futebol.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Futebol.Application.Applications;
public class PlayerApplication : IPlayerApplication
{
    private readonly IPlayerService _playerService;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMapper _mapper;

    public PlayerApplication(IPlayerService playerService, IHttpContextAccessor httpContext, IMapper mapper)
    {
        _playerService = playerService;
        _httpContext = httpContext;
        _mapper = mapper;
    }

    public async Task<ResponseModel<IEnumerable<PlayerModel>>> GetAllAsync()
    {
        var dtos = await _playerService.GetAllAsync();
        return _mapper.Map<ResponseModel<IEnumerable<PlayerModel>>>(dtos);
    }

    public async Task<ResponseModel<PlayerModel>> GetByIdAsync(Guid id)
    {
        var dto = await _playerService.GetByIdAsync(id);
        return _mapper.Map<ResponseModel<PlayerModel>>(dto);
    }

    public async Task<ResponseModel<PlayerModel>> UpdateAsync(UpdatePlayerModel model, Guid id)
    {
        var dto = await _playerService.UpdateAsync(_mapper.Map<UpdatePlayerDto>(model), id);
        return _mapper.Map<ResponseModel<PlayerModel>>(dto);
    }

    public async Task<ResponseModel<string>> UploadImageAsync(IFormFile model)
    {        
        var userEmail = $"{_httpContext.HttpContext?.User?.FindFirst(ClaimTypes.Email).Value}";
        var img = await _playerService.UploadImageAsync(model, userEmail);
        
        return _mapper.Map<ResponseModel<string>>(img);
    }
}
