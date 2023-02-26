namespace Futebol.Shared.Models.User;
public class TokenModel : ModelBase
{
    public string Token { get; set; }
    public DateTime ValidTo { get; set; }
}
