using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaceHomeEstateManagement.Contract.Service
{
    public interface IBlobStorageService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}