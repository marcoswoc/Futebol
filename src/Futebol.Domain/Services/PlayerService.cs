using AutoMapper;
using Futebol.Domain.Dto;
using Futebol.Domain.Dto.Player;
using Futebol.Domain.Repositories;
using Futebol.Domain.Services.Interfaces;
using Futebol.Infrastructure.Upload;
using Microsoft.AspNetCore.Http;

namespace Futebol.Domain.Services;
public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _repository;
    private readonly IUploadImage _uploadImage;
    private readonly IMapper _mapper;

    public PlayerService(IPlayerRepository repository, IUploadImage uploadImage, IMapper mapper)
    {
        _repository = repository;
        _uploadImage = uploadImage;
        _mapper = mapper;
    }

    public async Task<ResponseDto<IEnumerable<PlayerDto>>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return new() { Data = _mapper.Map<IEnumerable<PlayerDto>>(entities) };
    }

    public async Task<ResponseDto<PlayerDto>> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return new() { Data = _mapper.Map<PlayerDto>(entity) };
    }

    public async Task<ResponseDto<PlayerDto>> UpdateAsync(UpdatePlayerDto dto, Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        entity = _mapper.Map(dto, entity);

        await _repository.UpdateAsync(entity);

        return new() { Data = _mapper.Map<PlayerDto>(entity) };
    }

    public async Task<ResponseDto<string>> UploadImageAsync(IFormFile model, string fileName)
    {
        if (!model.ContentType.Contains("image"))
            return new() { Success = false, Message = "Upload apenas de imagens" };

        var result = await _uploadImage.UploadImageAsync(model, fileName);

        if (result.Item2 == true)
            return new() { Data = result.Item1 };

        return new() { Success = false, Message = "Erro Upload" };
    }

}
