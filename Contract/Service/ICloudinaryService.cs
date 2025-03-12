using System;
namespace PeaceHomeEstateManagement.Contract.Service
{
    public interface ICloudinaryService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<string> UploadVideoAsync(IFormFile file);
    }
}