using AutoMapper;
using Futebol.Application.Applications.Interfaces;
using Futebol.Application.Models;
using Futebol.Application.Models.Vote;
using Futebol.Domain.Dto.Vote;
using Futebol.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Futebol.Application.Applications;
public class VoteApplication : IVoteApplication
{
    private readonly IVoteService _voteService;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMapper _mapper;

    public VoteApplication(IVoteService voteService, IHttpContextAccessor httpContext, IMapper mapper)
    {
        _voteService = voteService;
        _httpContext = httpContext;
        _mapper = mapper;
    }

    public async Task<ResponseModel<VoteModel>> CreateAsync(CreateVoteModel model)
    {
        var email = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.Email).Value;

        var dto = await _voteService.CreateAsync(_mapper.Map<CreateVoteDto>(model), email);
        return _mapper.Map<ResponseModel<VoteModel>>(dto);
    }

    public async Task<ResponseModel<IEnumerable<VoteModel>>> GetAllAsync()
    {
        var email = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.Email).Value;

        var dtos = await _voteService.GetAllAsync(email);
        return _mapper.Map<ResponseModel<IEnumerable<VoteModel>>>(dtos);
    }

    public async Task<ResponseModel<IEnumerable<VoteAvarageModel>>> GetAllAverageAsync()
    {
        var dtos = await _voteService.GetAllAverageAsync();
        return _mapper.Map<ResponseModel<IEnumerable<VoteAvarageModel>>>(dtos);
    }

    public async Task<ResponseModel<VoteModel>> UpdateAsync(UpdateVoteModel model, Guid id)
    {
        var dto = await _voteService.UpdateAsync(_mapper.Map<UpdateVoteDto>(model), id);
        return _mapper.Map<ResponseModel<VoteModel>>(dto);
    }
}
