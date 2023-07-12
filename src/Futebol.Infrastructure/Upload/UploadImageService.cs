using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Futebol.Infrastructure.Upload;
public class UploadImageService : IUploadImage
{
    private readonly IConfiguration _configuration;

    public UploadImageService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<(string, bool)> UploadImageAsync(IFormFile model, string fileName)
    {
        var ext = model.ContentType.Split("/")[1];
        fileName = $"{fileName}.{ext}";
        var bucketName = _configuration["Amazon:Bucket"];
        PutObjectResponse? response = null;

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

        var sucess = response.HttpStatusCode == HttpStatusCode.OK;


        var url = $"https://{bucketName}.s3.sa-east-1.amazonaws.com/{fileName}";

        return new(url, sucess);
    }
}
