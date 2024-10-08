using Futebol.Api.Database;
using Futebol.Api.Entities;
using Futebol.Shared.Handlers;
using Futebol.Shared.Models;
using Futebol.Shared.Requests.Team;
using Futebol.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Futebol.Api.Handlers;

public class TeamHandler(ApplicationDbContext _context) : ITeamHandler
{
    public async Task<Response<TeamModel?>> CreateAsync(CreateTeamRequest request)
    {
        var team = new Team()
        {
            Name = request.Name,
        };

        try
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return new(team.Model(), 201, "Time criado com sucesso");
        }
        catch (Exception) 
        {
            return new(null, 500, "Não foi possível criar o time");
        }
    }

    public async Task<PagedResponse<List<TeamModel>?>> GetAllAsync(GetAllTeamsRequest request)
    {
        var query = _context
            .Teams
            .AsNoTracking();
        
        var teams = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var count = await query.CountAsync();
        var temasModel = teams.Select(x => x.Model()).ToList();

        return new(temasModel, count, request.PageNumber, request.PageSize);
    }
}
