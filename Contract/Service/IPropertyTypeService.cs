using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PeaceHomeEstateManagement.Dto;
using PeaceHomeEstateManagement.Models;

namespace PeaceHomeEstateManagement.Contract.Service
{
   public interface IPropertyTypeService
    {
        Task<PropertyTypeDto> CreateAsync(CreatePropertyTypeDto createPropertyTypeDto);
        Task<IList<PropertyTypeDto>> GetAllAsync();
        Task<PropertyTypeDto?> GetAsync(Guid id);
        Task<PropertyTypeDto?> GetAsync(Expression<Func<PropertyType, bool>> expression);
        Task<PropertyTypeDto> UpdateAsync(Guid id, CreatePropertyTypeDto updatePropertyTypeDto);
        Task DeleteAsync(Guid id);
    }
}