using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeaceHomeEstateManagement.Dto;

namespace PeaceHomeEstateManagement.Contract.Service
{
    public interface IAmenitiesService
    {
        Task<AmenitiesResponseDto> CreateAsync(CreateAmenitiesDto createAmenitiesDto);
        Task<IList<AmenitiesResponseDto>> GetAllAsync();
        Task<AmenitiesResponseDto?> GetAsync(Guid id);
        Task<AmenitiesResponseDto?> UpdateAsync(UpdateAmenitiesDto updateAmenitiesDto);
        Task DeleteAsync(Guid id);
    }

}