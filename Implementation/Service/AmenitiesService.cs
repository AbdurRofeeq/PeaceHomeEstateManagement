using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeaceHomeEstateManagement.Contract.Repository;
using PeaceHomeEstateManagement.Contract.Service;
using PeaceHomeEstateManagement.Dto;
using PeaceHomeEstateManagement.Models;

namespace PeaceHomeEstateManagement.Implementation.Service
{
    public class AmenitiesService : IAmenitiesService
    {
        private readonly IAmenitiesRepository _amenitiesRepository;

        public AmenitiesService(IAmenitiesRepository amenitiesRepository)
        {
            _amenitiesRepository = amenitiesRepository;
        }

        public async Task<AmenitiesResponseDto> CreateAsync(CreateAmenitiesDto createAmenitiesDto)
        {
            var amenities = new Amenities
            {
                Name = createAmenitiesDto.Name,
            };
            
            var createdAmenities = await _amenitiesRepository.CreateAsync(amenities);

            return new AmenitiesResponseDto
            {
                Id = createdAmenities.Id,
                Name = createdAmenities.Name,
                AmenitiesProperties = new List<AmenitiesPropertyDto>()  
            };
        }

        public async Task<IList<AmenitiesResponseDto>> GetAllAsync()
        {
            var amenities = await _amenitiesRepository.GetAllAsync();
            return amenities.Select(a => new AmenitiesResponseDto
            {
                Id = a.Id,
                Name = a.Name,
            }).ToList();
        }

        public async Task<AmenitiesResponseDto?> GetAsync(Guid id)
        {
            var amenities = await _amenitiesRepository.GetAsync(id);
            if (amenities == null) return null;

            return new AmenitiesResponseDto
            {
                Id = amenities.Id,
                Name = amenities.Name,
                AmenitiesProperties = amenities.AmenitiesProperties?
                    .Where(ap => ap.Property != null) // Filter out null Properties
                    .Select(ap => new AmenitiesPropertyDto
                    {
                        PropertyId = ap.PropertyId,
                        PropertyName = ap.Property?.Name ?? string.Empty // Null-coalescing
                    }).ToList() ?? new List<AmenitiesPropertyDto>() // Handle null collection
            };
        }

        public async Task<AmenitiesResponseDto?> UpdateAsync(UpdateAmenitiesDto updateAmenitiesDto)
        {
            var amenities = await _amenitiesRepository.GetAsync(updateAmenitiesDto.Id);
            if (amenities == null) return null;

            amenities.Name = updateAmenitiesDto.Name;

            var updatedAmenities = await _amenitiesRepository.UpdateAsync(amenities);

            return new AmenitiesResponseDto
            {
                Id = updatedAmenities.Id,
                Name = updatedAmenities.Name,
                AmenitiesProperties = updatedAmenities.AmenitiesProperties.Select(ap => new AmenitiesPropertyDto
                {
                    PropertyId = ap.PropertyId,
                    PropertyName = ap.Property.Name
                }).ToList()
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _amenitiesRepository.DeleteAsync(id);
        }
    }
}