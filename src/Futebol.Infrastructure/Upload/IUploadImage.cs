using Microsoft.AspNetCore.Http;

namespace Futebol.Infrastructure.Upload;
public interface IUploadImage
{
    Task<(string, bool)> UploadImageAsync(IFormFile model, string fileName);
}
