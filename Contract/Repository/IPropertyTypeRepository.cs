using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PeaceHomeEstateManagement.Models;

namespace PeaceHomeEstateManagement.Contract.Repository
{
    public interface IPropertyTypeRepository
    {
        Task<PropertyType> CreateAsync(PropertyType propertyType);
        Task<PropertyType> GetAsync(Guid id);
        Task<IList<PropertyType>> GetAllAsync();
        Task<PropertyType> UpdateAsync(PropertyType propertyType);
        Task<PropertyType> GetAsync(Expression<Func<PropertyType, bool>> expression);
        Task DeleteAsync(Guid id);
        Task<PropertyType?> GetByNameAsync(string name);
    }
}