using Futebol.Infrastructure.Models;

namespace Futebol.Domain.Entity;
public class Round : EntityBase
{
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Active { get; set; } = true;

    public bool IsActive()
    {
        var dateNow = DateTime.Now.Date;
        Active = dateNow >= StartDate.Date && dateNow <= EndDate.Date;
        return Active;
    }
}
