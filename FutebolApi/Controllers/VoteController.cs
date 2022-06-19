using FutebolApi.Models;
using FutebolApi.Models.Vote;
using FutebolApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FutebolApi.Controllers;
[Route("api/[controller]")]
[ApiController]
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
        return Ok(await _voteService.CreateAsync(model));
    }

    [HttpGet]
    public async Task<ActionResult<ResponseModel<IEnumerable<VoteModel>>>> GetAllAsync()
    {
        return Ok(await _voteService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel<VoteModel>>> GetByIdAsync([FromRoute] Guid id)
    {
        return Ok(await _voteService.GetByIdAsync(id));
    }
}
