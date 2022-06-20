using AutoMapper;
using FutebolApi.Entity;
using FutebolApi.Models.Player;
using FutebolApi.Models.Round;
using FutebolApi.Models.Vote;

namespace FutebolApi;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Player, PlayerModel>().ReverseMap();
        CreateMap<Player, UpdatePlayerModel>().ReverseMap();
        
        CreateMap<Round, RoundModel>().ReverseMap();
        CreateMap<Round, CreateRoundModel>().ReverseMap();

        CreateMap<Vote, VoteModel>().ReverseMap();
        CreateMap<Vote, CreateVoteModel>().ReverseMap();
        CreateMap<Vote, UpdateVoteModel>().ReverseMap();
    }
}
