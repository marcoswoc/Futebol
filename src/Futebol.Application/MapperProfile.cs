using AutoMapper;
using Futebol.Domain.Dto;
using Futebol.Domain.Dto.Player;
using Futebol.Domain.Dto.Round;
using Futebol.Domain.Dto.User;
using Futebol.Domain.Dto.Vote;
using Futebol.Shared.Models;
using Futebol.Shared.Models.Player;
using Futebol.Shared.Models.Round;
using Futebol.Shared.Models.User;
using Futebol.Shared.Models.Vote;

namespace Futebol.Application.Models;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<PlayerDto, PlayerModel>().ReverseMap();
        CreateMap<UpdatePlayerDto, UpdatePlayerModel>().ReverseMap();
        CreateMap<Domain.Entity.Player, PlayerDto>().ReverseMap();
        CreateMap<Domain.Entity.Player, UpdatePlayerDto>().ReverseMap();

        CreateMap<RoundDto, RoundModel>().ReverseMap();
        CreateMap<CreateRoundDto, CreateRoundModel>().ReverseMap();
        CreateMap<Domain.Entity.Round, RoundDto>().ReverseMap();
        CreateMap<Domain.Entity.Round, CreateRoundDto>().ReverseMap();

        CreateMap<VoteDto, VoteModel>().ReverseMap();
        CreateMap<CreateVoteDto, CreateVoteModel>().ReverseMap();
        CreateMap<UpdateVoteDto, UpdateVoteModel>().ReverseMap();
        CreateMap<Domain.Entity.Vote, VoteDto>().ReverseMap();
        CreateMap<Domain.Entity.Vote, CreateVoteDto>().ReverseMap();
        CreateMap<Domain.Entity.Vote, UpdateVoteDto>().ReverseMap();

        CreateMap<CreateUserDto, CreateUserModel>().ReverseMap();
        CreateMap<LoginDto, LoginModel>().ReverseMap();
        CreateMap<ResetPasswordDto, ResetPasswordModel>().ReverseMap();
        CreateMap<TokenDto, TokenModel>().ReverseMap();

        CreateMap<VoteAvarageDto, VoteAvarageModel>().ReverseMap();
        CreateMap<VoteAttributesDto, VoteAttributesModel>().ReverseMap();
        CreateMap<ResponseDto, ResponseModel>().ReverseMap();
        CreateMap(typeof(ResponseDto<>), typeof(ResponseModel<>)).ReverseMap();
    }
}
