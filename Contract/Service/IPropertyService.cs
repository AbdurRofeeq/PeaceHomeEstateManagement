using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeaceHomeEstateManagement.Dto;
using PeaceHomeEstateManagement.Paging;

namespace PeaceHomeEstateManagement.Contract.Service
{
    public interface IPropertyService
    {
        Task<PropertyResponseDto> CreateAsync(CreatePropertyDto createPropertyDto);
        Task<PaginatedList<PropertyResponseDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<PropertyResponseDto?> GetAsync(Guid id);
        Task<PropertyResponseDto?> UpdateAsync(UpdatePropertyDto updatePropertyDto);
        Task DeleteAsync(Guid id);
        Task<bool> PropertyNameExistsAsync(string name);
    }
}