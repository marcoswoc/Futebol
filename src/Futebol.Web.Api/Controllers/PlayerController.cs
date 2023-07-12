using Futebol.Application.Applications.Interfaces;
using Futebol.Shared.Models;
using Futebol.Shared.Models.Player;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Futebol.Web.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PlayerController : ControllerBase
{
    private readonly IPlayerApplication _playerApplication;

    public PlayerController(IPlayerApplication playerApplication)
    {
        _playerApplication = playerApplication;
    }


    [HttpGet("with-user")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ResponseModel<IEnumerable<PlayerWithUserModel>>>> GetAllPlayersWithUsersAsync()
    {
        return Ok(await _playerApplication.GetAllPlayersWithUsersAsync());
    }


    [HttpGet]
    public async Task<ActionResult<ResponseModel<IEnumerable<PlayerModel>>>> GetAllAsync()
    {
        return Ok(await _playerApplication.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel<PlayerModel>>> GetByIdAsync([FromRoute] Guid id)
    {
        return Ok(await _playerApplication.GetByIdAsync(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseModel<PlayerModel>>> UpdateAsync([FromBody] UpdatePlayerModel model, [FromRoute] Guid id)
    {
        return Ok(await _playerApplication.UpdateAsync(model, id));
    }

    [HttpPost("image")]
    [RequestSizeLimit(52428800)]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
        return Ok(await _playerApplication.UploadImageAsync(file));
    }
}
