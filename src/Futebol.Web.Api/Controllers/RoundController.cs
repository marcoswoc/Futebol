using Futebol.Application.Applications.Interfaces;
using Futebol.Shared.Models;
using Futebol.Shared.Models.Round;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Futebol.Web.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoundController : ControllerBase
{
    private readonly IRoundApplication _roundApplication;

    public RoundController(IRoundApplication roundApplication)
    {
        _roundApplication = roundApplication;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ResponseModel<RoundModel>>> CreateAsync([FromBody] CreateRoundModel model)
    {
        return Ok(await _roundApplication.CreateAsync(model));
    }

    [HttpGet]
    public async Task<ActionResult<ResponseModel<IEnumerable<RoundModel>>>> GetAllAsync()
    {
        return Ok(await _roundApplication.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel<RoundModel>>> GetByIdAsync([FromRoute] Guid id)
    {
        return Ok(await _roundApplication.GetByIdAsync(id));
    }
}
