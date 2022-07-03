using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using FutebolApi.Data.Repositories.Interfaces;
using FutebolApi.Models;
using FutebolApi.Models.Player;
using FutebolApi.Services.Interfaces;
using System.Net;
using System.Security.Claims;

namespace FutebolApi.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMapper _mapper;

    public PlayerService(IPlayerRepository repository, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _mapper = mapper;
        _configuration = configuration;
        _httpContext = httpContext;
    }

    public async Task<ResponseModel<IEnumerable<PlayerModel>>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return new() { Data = _mapper.Map<IEnumerable<PlayerModel>>(entities) };
    }  

    public async Task<ResponseModel<PlayerModel>> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return new() { Data = _mapper.Map<PlayerModel>(entity) };
    }

    public async Task<ResponseModel<PlayerModel>> UpdateAsync(UpdatePlayerModel model, Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        entity = _mapper.Map(model, entity);

        await _repository.UpdateAsync(entity);

        return new() { Data = _mapper.Map<PlayerModel>(entity) };
    }

    public async Task<ResponseModel<string>> UploadImageAsync(IFormFile model)
    {
        var ext = model.ContentType.Split("/")[1];
        var fileName = $"{_httpContext.HttpContext?.User?.FindFirst(ClaimTypes.Email).Value.Split("@").First()}.{ext}";
        var bucketName = _configuration["Amazon:Bucket"];
        PutObjectResponse response = null;

        if (!model.ContentType.Contains("image"))
            return new() { Message = "Upload apenas de imagens" };

        using var client = new AmazonS3Client(_configuration["Amazon:AccessKey"], _configuration["Amazon:AccessSecret"], Amazon.RegionEndpoint.SAEast1);
        var exists = await client.ListObjectsAsync(bucketName);        

        if (exists.S3Objects.FirstOrDefault(x => x.Key == fileName) is not null)
            await client.DeleteObjectAsync(bucketName, fileName);

        byte[] fileBytes = new Byte[model.Length];
        model.OpenReadStream().Read(fileBytes, 0, Int32.Parse(model.Length.ToString()));       

        using (var stream = new MemoryStream(fileBytes))
        {
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = fileName,
                InputStream = stream,
                ContentType = model.ContentType,
                CannedACL = S3CannedACL.PublicRead
            };

            response = await client.PutObjectAsync(request);
        }

        var imgUrl = $"https://{bucketName}.s3.sa-east-1.amazonaws.com/{fileName}";

        if (response.HttpStatusCode == HttpStatusCode.OK)
            return new() { Data = imgUrl };

        return new() { Message = "Erro Upload" };

    }
}
