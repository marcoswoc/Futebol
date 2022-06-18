using FutebolApi.Models;
using FutebolApi.Models.Round;
using FutebolApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FutebolApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoundController : ControllerBase
{
    private readonly IRoundService _roundService;

    public RoundController(IRoundService roundService)
    {
        _roundService = roundService;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel<RoundModel>>> CreateAsync([FromBody] CreateRoundModel model)
    {
        return Ok(await _roundService.CreateAsync(model));
    }

    [HttpGet]
    public async Task<ActionResult<ResponseModel<IEnumerable<RoundModel>>>> GetAllAsync()
    {
        return Ok(await _roundService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel<RoundModel>>> GetByIdAsync([FromRoute] Guid id)
    {
        return Ok(await _roundService.GetByIdAsync(id));
    }
}
