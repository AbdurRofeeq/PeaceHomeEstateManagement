using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using PeaceHomeEstateManagement.Contract.Service;

namespace PeaceHomeEstateManagement.Implementation.Service
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            _connectionString = configuration["AzureBlobStorage:ConnectionString"];
            _containerName = configuration["AzureBlobStorage:ContainerName"];
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var blobClient = new BlobContainerClient(_connectionString, _containerName);
            await blobClient.CreateIfNotExistsAsync();

            var blobName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var blob = blobClient.GetBlobClient(blobName);

            using (var stream = file.OpenReadStream())
            {
                await blob.UploadAsync(stream);
            }
            return blob.Uri.ToString();
        }

        // public async Task<string> UploadAsync(IFormFile file)
        // {
        //     if (file == null || file.Length == 0)
        //         return null;

        //     // Validate file size (e.g., 5MB limit)
        //     if (file.Length > 5 * 1024 * 1024) // 5MB
        //     {
        //         throw new ArgumentException("File size exceeds the limit of 5MB.");
        //     }

        //     // Validate file type (e.g., only images and videos)
        //     var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".mp4" };
        //     var fileExtension = Path.GetExtension(file.FileName).ToLower();
        //     if (!allowedExtensions.Contains(fileExtension))
        //     {
        //         throw new ArgumentException("Invalid file type. Only images and videos are allowed.");
        //     }

        //     var blobClient = new BlobContainerClient(_connectionString, _containerName);
        //     await blobClient.CreateIfNotExistsAsync();

        //     var blobName = Guid.NewGuid() + fileExtension;
        //     var blob = blobClient.GetBlobClient(blobName);

        //     using (var stream = file.OpenReadStream())
        //     {
        //         await blob.UploadAsync(stream);
        //     }
        //     return blob.Uri.ToString();
        // }
    }
}