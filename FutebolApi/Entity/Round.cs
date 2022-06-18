namespace FutebolApi.Entity;

public class Round
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Active { get; set; }
}
