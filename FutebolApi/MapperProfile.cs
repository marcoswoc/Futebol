using AutoMapper;
using FutebolApi.Entity;
using FutebolApi.Models.Player;

namespace FutebolApi;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Player, PlayerModel>().ReverseMap();
        CreateMap<Player, UpdatePlayerModel>().ReverseMap();
    }
}
