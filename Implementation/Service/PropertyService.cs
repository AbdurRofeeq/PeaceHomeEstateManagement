using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeaceHomeEstateManagement.Contract.Repository;
using PeaceHomeEstateManagement.Contract.Service;
using PeaceHomeEstateManagement.Dto;
using PeaceHomeEstateManagement.Models;
using PeaceHomeEstateManagement.Paging;

namespace PeaceHomeEstateManagement.Implementation.Service
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IAmenitiesRepository _amenitiesRepository;

        public PropertyService(IPropertyRepository propertyRepository, IAmenitiesRepository amenitiesRepository)
        {
            _propertyRepository = propertyRepository;
            _amenitiesRepository = amenitiesRepository;
        }

        public async Task<PropertyResponseDto> CreateAsync(CreatePropertyDto createPropertyDto)
        {
            var amenities = await _amenitiesRepository.GetListAsync(a => createPropertyDto.AmenitiesIds.Contains(a.Id));

            var property = new Property
            {
                Name = createPropertyDto.Name,
                Description = createPropertyDto.Description,
                Image1 = createPropertyDto.Image1,
                Image2 = createPropertyDto.Image2,
                Image3 = createPropertyDto.Image3,
                Video = createPropertyDto.Video,
                Address = createPropertyDto.Address,
                PropertyTypeId = createPropertyDto.PropertyTypeId,

                AmenitiesProperties = amenities.Select(a => new AmenitiesProperty
                {
                    AmenitiesId = a.Id
                }).ToList()
            };

            var createdProperty = await _propertyRepository.CreateAsync(property);

            return new PropertyResponseDto
            {
                Id = createdProperty.Id,
                Name = createdProperty.Name,
                Description = createdProperty.Description,
                Image1 = createdProperty.Image1,
                Image2 = createdProperty.Image2,
                Image3 = createdProperty.Image3,
                Video = createdProperty.Video,
                PropertyType = createdProperty.PropertyType.Name,
                Amenities = createdProperty.AmenitiesProperties.Select(ap => new AmenitiesResponseDto
                {
                    Id = ap.Amenities.Id,
                    Name = ap.Amenities.Name
                }).ToList()
            };
        }

        public async Task<PaginatedList<PropertyResponseDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var paginatedProperties = await _propertyRepository.GetAllAsync(pageNumber, pageSize);

            var paginatedPropertyResponse = new PaginatedList<PropertyResponseDto>
            {
                Items = paginatedProperties.Items.Select(p => new PropertyResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Image1 = p.Image1,
                    Image2 = p.Image2,
                    Image3 = p.Image3,
                    Video = p.Video,
                    PropertyType = p.PropertyType.Name,
                    Amenities = p.AmenitiesProperties.Select(ap => new AmenitiesResponseDto
                    {
                        Id = ap.Amenities.Id,
                        Name = ap.Amenities.Name
                    }).ToList()
                }).ToList(),
                TotalItems = paginatedProperties.TotalItems,
                Page = paginatedProperties.Page,
                PageSize = paginatedProperties.PageSize
            };

            return paginatedPropertyResponse;
        }

        public async Task<PropertyResponseDto?> GetAsync(Guid id)
        {
            var property = await _propertyRepository.GetAsync(id);
            if (property == null) return null;

            return new PropertyResponseDto
            {
                Id = property.Id,
                Name = property.Name,
                Description = property.Description,
                Image1 = property.Image1,
                Image2 = property.Image2,
                Image3 = property.Image3,
                Video = property.Video,
                PropertyType = property.PropertyType.Name,
                Amenities = property.AmenitiesProperties.Select(ap => new AmenitiesResponseDto
                {
                    Id = ap.Amenities.Id,
                    Name = ap.Amenities.Name
                }).ToList()
            };
        }

        public async Task<PropertyResponseDto?> UpdateAsync(UpdatePropertyDto updatePropertyDto)
        {
            var property = await _propertyRepository.GetAsync(updatePropertyDto.Id);
            if (property == null) return null;

            var amenities = await _amenitiesRepository.GetListAsync(a => updatePropertyDto.AmenitiesIds.Contains(a.Id));

            property.Name = updatePropertyDto.Name;
            property.Description = updatePropertyDto.Description;
            property.Image1 = updatePropertyDto.Image1;
            property.Image2 = updatePropertyDto.Image2;
            property.Image3 = updatePropertyDto.Image3;
            property.Video = updatePropertyDto.Video;
            property.PropertyTypeId = updatePropertyDto.PropertyTypeId;
            property.AmenitiesProperties = amenities.Select(a => new AmenitiesProperty
            {
                AmenitiesId = a.Id
            }).ToList();

            var updatedProperty = await _propertyRepository.UpdateAsync(property);

            return new PropertyResponseDto
            {
                Id = updatedProperty.Id,
                Name = updatedProperty.Name,
                Description = updatedProperty.Description,
                Image1 = updatedProperty.Image1,
                Image2 = updatedProperty.Image2,
                Image3 = updatedProperty.Image3,
                Video = updatedProperty.Video,
                PropertyType = updatedProperty.PropertyType.Name,
                Amenities = updatedProperty.AmenitiesProperties.Select(ap => new AmenitiesResponseDto
                {
                    Id = ap.Amenities.Id,
                    Name = ap.Amenities.Name
                }).ToList()
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _propertyRepository.DeleteAsync(id);
        }
    }

}