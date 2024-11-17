using Futebol.Api.Database;
using Futebol.Api.Entities;
using Futebol.Api.Entities.Account;
using Futebol.Shared.Handlers;
using Futebol.Shared.Models;
using Futebol.Shared.Requests.Player;
using Futebol.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Futebol.Api.Handlers;

public class PlayerHandler(
    ApplicationDbContext _context,
    UserManager<User> _userManager) : IPlayerHandler
{
    public async Task<Response<PlayerModel?>> CreateAsync(CreatePlayerRequest request)
    {
        var user = new User()
        {
            UserName = request.Email,
            Email = request.Email,
        };

        try
        {
            var userResult = await _userManager.CreateAsync(user, request.Password);

            if (userResult is null || userResult.Succeeded is false)
                return new Response<PlayerModel?>(null, 500, "Erro ao criar jogador");

            user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return new Response<PlayerModel?>(null, 500, "Erro ao criar jogador");

            var player = new Player()
            {
                Name = request.Name,
                UserId = user.Id
            };

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            return new(player.Model(), 201, "Jogador criado com sucesso");
        }
        catch (Exception) 
        {
            return new(null, 500, "Não foi possível criar o jogador");
        }
    }

    public async Task<PagedResponse<List<PlayerModel>?>> GetAllAsync(GetAllPlayersRequest request)
    {
        var query = _context
            .Players
            .OrderBy(o => o.Name)
            .AsNoTracking();

        var players = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var count = await query.CountAsync();
        var playersModel = players.Select(x => x.Model()).ToList();

        return new(playersModel, count, request.PageNumber, request.PageSize);
    }

    public async Task<Response<PlayerModel?>> GetByIdAsync(GetByIdPlayerRequest request)
    {
        try
        {
            var player = await _context
                .Players
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return player is null
                ? new(null, 404, "Jogador não encontrado")
                : new(player.Model());
        }
        catch (Exception)
        {
            return new(null, 500, "Não foi possível obter o Jogador");
        }
    }

    public async Task<Response<PlayerModel?>> UpdateAsync(UpdatePlayerRequest request)
    {
        try
        {
            var player = await _context
                .Players
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (player is null)
                return new(null, 404, "Jogador não encontrado");

            player.Name = request.Name;

            _context.Players.Update(player);
            await _context.SaveChangesAsync();

            return new(player.Model(), message: "Jogador atualizada com sucesso");
        }
        catch (Exception)
        {
            return new(null, 500, "Não foi possível atualizar o Jogador");
        }
    }

    public async Task<Response<PlayerModel?>> DeleteAsync(DeletePlayerRequest request)
    {
        try
        {
            var player = await _context
                .Players
                .Include(i => i.User)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (player is null)
                return new(null, 404, "Jogador não encontrado");

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            await _userManager.DeleteAsync(player.User);

            return new(player.Model(), message: "Jogador excluido com sucesso");

        }
        catch (Exception)
        {
            return new(null, 500, "Não foi possível deletar o Jogador");
        }
    }
}
