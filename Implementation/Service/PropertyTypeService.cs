using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PeaceHomeEstateManagement.Contract.Repository;
using PeaceHomeEstateManagement.Contract.Service;
using PeaceHomeEstateManagement.Dto;
using PeaceHomeEstateManagement.Models;

namespace PeaceHomeEstateManagement.Implementation.Service
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;

        public PropertyTypeService(IPropertyTypeRepository propertyTypeRepository)
        {
            _propertyTypeRepository = propertyTypeRepository;
        }

        public async Task<PropertyTypeDto> CreateAsync(CreatePropertyTypeDto createPropertyTypeDto)
        {
            var existingPropertyType = await _propertyTypeRepository.GetByNameAsync(createPropertyTypeDto.Name);
            if (existingPropertyType != null)
            {
                throw new InvalidOperationException("A property type with this name already exists.");
            }
               
            var propertyType = new PropertyType
            {
                Name = createPropertyTypeDto.Name
            };

            var createdPropertyType = await _propertyTypeRepository.CreateAsync(propertyType);

            return new PropertyTypeDto
            {
                Id = createdPropertyType.Id,
                Name = createdPropertyType.Name,
                Properties = createdPropertyType.Properties.Select(p => new PropertyDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description
                }).ToList()
            };
        }

        public async Task<IList<PropertyTypeDto>> GetAllAsync()
        {
            var propertyTypes = await _propertyTypeRepository.GetAllAsync();

            return propertyTypes.Select(pt => new PropertyTypeDto
            {
                Id = pt.Id,
                Name = pt.Name,
                Properties = pt.Properties.Select(p => new PropertyDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description
                }).ToList()
            }).ToList();
        }

        public async Task<PropertyTypeDto?> GetAsync(Guid id)
        {
            var propertyType = await _propertyTypeRepository.GetAsync(id);

            if (propertyType == null) return null;

            return new PropertyTypeDto
            {
                Id = propertyType.Id,
                Name = propertyType.Name,
                Properties = propertyType.Properties.Select(p => new PropertyDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description
                }).ToList()
            };
        }

        public async Task<PropertyTypeDto?> GetAsync(Expression<Func<PropertyType, bool>> expression)
        {
            var propertyType = await _propertyTypeRepository.GetAsync(expression);

            if (propertyType == null) return null;

            return new PropertyTypeDto
            {
                Id = propertyType.Id,
                Name = propertyType.Name,
                Properties = propertyType.Properties.Select(p => new PropertyDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description
                }).ToList()
            };
        }

        public async Task<PropertyTypeDto> UpdateAsync(Guid id, CreatePropertyTypeDto updatePropertyTypeDto)
        {
            var propertyType = await _propertyTypeRepository.GetAsync(id);

            if (propertyType == null)
                throw new Exception("PropertyType not found");

            propertyType.Name = updatePropertyTypeDto.Name;

            var updatedPropertyType = await _propertyTypeRepository.UpdateAsync(propertyType);

            return new PropertyTypeDto
            {
                Id = updatedPropertyType.Id,
                Name = updatedPropertyType.Name,
                Properties = updatedPropertyType.Properties.Select(p => new PropertyDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description
                }).ToList()
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _propertyTypeRepository.DeleteAsync(id);
        }
    }
}