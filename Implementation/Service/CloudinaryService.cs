using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PeaceHomeEstateManagement.Contract.Service;
using System;
using System.Threading.Tasks;

namespace PeaceHomeEstateManagement.Implementation.Service
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<CloudinaryService> _logger;

        public CloudinaryService(IConfiguration configuration, ILogger<CloudinaryService> logger)
        {
            var cloudName = configuration["Cloudinary:CloudName"];
            var apiKey = configuration["Cloudinary:ApiKey"];
            var apiSecret = configuration["Cloudinary:ApiSecret"];

            // Validate Cloudinary credentials
            if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                throw new Exception("Cloudinary credentials are missing or invalid.");
            }

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
            _logger = logger;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                _logger.LogError("File is null or empty.");
                throw new ArgumentException("File is null or empty.");
            }

            try
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = Guid.NewGuid().ToString() // Unique public ID for the file
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult == null)
                {
                    _logger.LogError("Cloudinary upload failed: uploadResult is null.");
                    throw new Exception("Cloudinary upload failed: uploadResult is null.");
                }

                if (uploadResult.SecureUrl == null)
                {
                    _logger.LogError("Cloudinary upload failed: SecureUrl is null.");
                    throw new Exception("Cloudinary upload failed: SecureUrl is null.");
                }

                return uploadResult.SecureUrl.ToString(); // Return the secure URL
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload file to Cloudinary.");
                throw new Exception("Failed to upload file to Cloudinary.", ex);
            }
        }

        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                _logger.LogError("File is null or empty.");
                throw new ArgumentException("File is null or empty.");
            }

            try
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = Guid.NewGuid().ToString() // Unique public ID for the file
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult == null)
                {
                    _logger.LogError("Cloudinary upload failed: uploadResult is null.");
                    throw new Exception("Cloudinary upload failed: uploadResult is null.");
                }

                if (uploadResult.SecureUrl == null)
                {
                    _logger.LogError("Cloudinary upload failed: SecureUrl is null.");
                    throw new Exception("Cloudinary upload failed: SecureUrl is null.");
                }

                return uploadResult.SecureUrl.ToString(); // Return the secure URL
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload video to Cloudinary.");
                throw new Exception("Failed to upload video to Cloudinary.", ex);
            }
        }
    }
}