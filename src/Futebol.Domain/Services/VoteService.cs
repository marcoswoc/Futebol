using AutoMapper;
using Futebol.Domain.Dto;
using Futebol.Domain.Dto.Player;
using Futebol.Domain.Dto.Vote;
using Futebol.Domain.Entity;
using Futebol.Domain.Repositories;
using Futebol.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Futebol.Domain.Services;
public class VoteService : IVoteService
{
    private readonly IVoteRepository _repository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IRoundRepository _roundRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public VoteService(
        IVoteRepository repository,
        IPlayerRepository playerRepository,
        IRoundRepository roundRepository,
        UserManager<User> userManager,
        IMapper mapper)
    {
        _repository = repository;
        _playerRepository = playerRepository;
        _userManager = userManager;
        _roundRepository = roundRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDto<VoteDto>> CreateAsync(CreateVoteDto dto, string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);

        var voter = await _playerRepository.FindExpressionAsync(x => x.User.Id == user.Id);
        if (voter is null)
            return new() { Success = false, Message = "Usuario logado não tem um player cadastrado" };

        var player = await _playerRepository.GetByIdAsync(dto.PlayerId);
        if (player is null)
            return new() { Success = false, Message = "Player não encontrado" };

        if (voter.First().Id == player.Id)
            return new() { Success = false, Message = "Não é possivel votar em si mesmo" };

        var round = await _roundRepository.GetByIdAsync(dto.RoundId);
        if (round is null)
            return new() { Success = false, Message = "Round não encontrado" };

        if (round.IsActive() is false)
            return new() { Success = false, Message = "Round finalizado" };

        var exists = await _repository.FindExpressionAsync(x => x.User.Id == user.Id && x.Player.Id == player.Id && x.Round.Id == round.Id);
        if (exists.Any())
            return new() { Success = false, Message = "Voto já realizado" };

        var entity = _mapper.Map<Vote>(dto);
        entity.User = user;
        entity.Player = player;
        entity.Round = round;

        entity = await _repository.CreateAsync(entity);

        return new() { Data = _mapper.Map<VoteDto>(entity) };
    }

    public async Task<ResponseDto<IEnumerable<VoteDto>>> GetAllAsync(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);

        var entities = await _repository.FindExpressionAsync(x => x.User.Id == user.Id);
        return new() { Data = _mapper.Map<IEnumerable<VoteDto>>(entities) };
    }

    public async Task<ResponseDto<VoteDto>> UpdateAsync(UpdateVoteDto dto, Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        entity = _mapper.Map(dto, entity);

        await _repository.UpdateAsync(entity);

        return new() { Data = _mapper.Map<VoteDto>(entity) };
    }

    public async Task<ResponseDto<IEnumerable<VoteAvarageDto>>> GetAllAverageAsync()
    {
        var entities = await _repository.GetAllAsync();

        var result = entities.GroupBy(x => x.Player);
        var list = new List<VoteAvarageDto>();

        foreach (var userMedia in result)
        {
            var attributes = new List<VoteAttributesDto>();

            var defense = userMedia.Average(x => x.Defense);
            var attack = userMedia.Average(x => x.Attack);
            var kick = userMedia.Average(x => x.Kick);
            var velocity = userMedia.Average(x => x.Velocity);
            var pass = userMedia.Average(x => x.Pass);
            var generalAverage = (defense + attack + kick + velocity + pass) / 5;

            attributes.Add(new() { Name = nameof(defense), Value = defense });
            attributes.Add(new() { Name = nameof(attack), Value = attack });
            attributes.Add(new() { Name = nameof(kick), Value = kick });
            attributes.Add(new() { Name = nameof(velocity), Value = velocity });
            attributes.Add(new() { Name = nameof(pass), Value = pass });
            attributes.Add(new() { Name = nameof(generalAverage), Value = generalAverage });

            var avarage = new VoteAvarageDto
            {
                Player = _mapper.Map<PlayerDto>(userMedia.Key),
                VoteAttributes = attributes
            };         

            list.Add(avarage);
        }

        return new() { Data = list };
    }
}
