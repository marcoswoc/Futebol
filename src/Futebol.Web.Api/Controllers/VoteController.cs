using Futebol.Application.Applications.Interfaces;
using Futebol.Shared.Models;
using Futebol.Shared.Models.Vote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Futebol.Web.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class VoteController : ControllerBase
{
    private readonly IVoteApplication _voteApplication;

    public VoteController(IVoteApplication voteApplication)
    {
        _voteApplication = voteApplication;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel<VoteModel>>> CreateAsync([FromBody] CreateVoteModel model)
    {
        var result = await _voteApplication.CreateAsync(model);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet]
    public async Task<ActionResult<ResponseModel<IEnumerable<VoteModel>>>> GetAllAsync()
    {
        return Ok(await _voteApplication.GetAllAsync());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseModel<VoteModel>>> UpdateAsync([FromBody] UpdateVoteModel model, [FromRoute] Guid id)
    {
        return Ok(await _voteApplication.UpdateAsync(model, id));
    }

    [HttpGet("average")]
    public async Task<ActionResult<ResponseModel<IEnumerable<VoteAvarageModel>>>> GetAllAverageAsync()
    {
        return Ok(await _voteApplication.GetAllAverageAsync());
    }
}
