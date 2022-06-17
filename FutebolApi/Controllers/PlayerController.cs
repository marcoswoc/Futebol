using FutebolApi.Models;
using FutebolApi.Models.Player;
using FutebolApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutebolApi.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseModel<IEnumerable<PlayerModel>>>> GetAllAsync()
    {
        return Ok(await _playerService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel<PlayerModel>>> GetByIdAsync([FromRoute] Guid id)
    {
        return Ok(await _playerService.GetByIdAsync(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseModel<PlayerModel>>> UpdateAsync([FromBody] UpdatePlayerModel model, [FromRoute] Guid id)
    {
        return Ok(await _playerService.UpdateAsync(model, id));
    }
}
