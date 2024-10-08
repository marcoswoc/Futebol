using Futebol.Shared.Models;

namespace Futebol.Api.Entities;

public class Team
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public TeamModel Model()
    {
        return new() { Id = Id, Name = Name };
    }
}