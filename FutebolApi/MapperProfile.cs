using AutoMapper;
using FutebolApi.Entity;
using FutebolApi.Models.Player;
using FutebolApi.Models.Round;

namespace FutebolApi;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Player, PlayerModel>().ReverseMap();
        CreateMap<Player, UpdatePlayerModel>().ReverseMap();
        
        CreateMap<Round, RoundModel>().ReverseMap();
        CreateMap<Round, CreateRoundModel>().ReverseMap();
    }
}
