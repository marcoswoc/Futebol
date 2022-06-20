using AutoMapper;
using FutebolApi.Data.Repositories.Interfaces;
using FutebolApi.Entity;
using FutebolApi.Models;
using FutebolApi.Models.Vote;
using FutebolApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FutebolApi.Services;

public class VoteService : IVoteService
{
    private readonly IVoteRepository _repository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IRoundRepository _roundRepository;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;

    public VoteService(
        IVoteRepository repository,
        IHttpContextAccessor httpContext,
        IMapper mapper,
        IPlayerRepository playerRepository,
        IRoundRepository roundRepository,
        UserManager<IdentityUser> userManager)
    {
        _repository = repository;
        _mapper = mapper;
        _httpContext = httpContext;
        _playerRepository = playerRepository;
        _userManager = userManager;
        _roundRepository = roundRepository;
    }

    public async  Task<ResponseModel<VoteModel>> CreateAsync(CreateVoteModel model)
    {
        var userEmail = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.Email).Value;
        var user = await _userManager.FindByEmailAsync(userEmail);

        var voter = await _playerRepository.FindExpressionAsync(x => x.User.Id == user.Id);
        if (voter is null)
            return new() { Success = false, Message = "Usuario logado não tem um player cadastrado" };

        var player = await _playerRepository.GetByIdAsync(model.PlayerId);
        if (player is null)
            return new() { Success = false, Message = "Player não encontrado" };

        if(voter.First().Id == player.Id)
            return new() { Success = false, Message = "Não é possivel votar em si mesmo" };

        var round = await _roundRepository.GetByIdAsync(model.RoundId);
        if (round is null)
            return new() { Success = false, Message = "Round não encontrado" };

        if (round.IsActive() is false)
            return new() { Success = false, Message = "Round finalizado" };

        var exists = await _repository.FindExpressionAsync(x => x.User.Id == user.Id && x.Player.Id == player.Id && x.Round.Id == round.Id);
        if (exists.Any())
            return new() { Success = false, Message = "Voto já realizado" };

        var entity = _mapper.Map<Vote>(model);
        entity.User = user;
        entity.Player = player;
        entity.Round = round;

        entity = await _repository.CreateAsync(entity);

        return new() { Data = _mapper.Map<VoteModel>(entity) };
    }

    public async Task<ResponseModel<IEnumerable<VoteModel>>> GetAllAsync()
    {
        var userEmail = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.Email).Value;
        var user = await _userManager.FindByEmailAsync(userEmail);

        var entities = await _repository.FindExpressionAsync(x => x.User.Id == user.Id);
        return new() { Data = _mapper.Map<IEnumerable<VoteModel>>(entities) };
    }

    public async Task<ResponseModel<VoteModel>> UpdateAsync(UpdateVoteModel model, Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        entity = _mapper.Map(model, entity);

        await _repository.UpdateAsync(entity);

        return new() { Data = _mapper.Map<VoteModel>(entity) };
    }
}
