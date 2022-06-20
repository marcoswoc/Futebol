using FutebolApi.Models;
using FutebolApi.Models.Vote;
using FutebolApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutebolApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class VoteController : ControllerBase
{
    private readonly IVoteService _voteService;

    public VoteController(IVoteService voteService)
    {
        _voteService = voteService;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel<VoteModel>>> CreateAsync([FromBody] CreateVoteModel model)
    {
        var result = await _voteService.CreateAsync(model);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet]
    public async Task<ActionResult<ResponseModel<IEnumerable<VoteModel>>>> GetAllAsync()
    {
        return Ok(await _voteService.GetAllAsync());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseModel<VoteModel>>> UpdateAsync([FromBody] UpdateVoteModel model, [FromRoute] Guid id)
    {
        return Ok(await _voteService.UpdateAsync(model, id));    
    }
}
