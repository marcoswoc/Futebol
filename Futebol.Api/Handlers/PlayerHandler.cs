using Futebol.Api.Database;
using Futebol.Api.Entities;
using Futebol.Api.Entities.Account;
using Futebol.Shared.Handlers;
using Futebol.Shared.Models;
using Futebol.Shared.Requests.Player;
using Futebol.Shared.Responses;
using Microsoft.AspNetCore.Identity;

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
}
