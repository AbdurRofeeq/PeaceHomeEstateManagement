using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PeaceHomeEstateManagement.Models;
using PeaceHomeEstateManagement.Paging;

namespace PeaceHomeEstateManagement.Contract.Repository
{
    public interface IPropertyRepository
    {
        Task<Property> CreateAsync(Property property);
        Task<Property> GetAsync(Guid id);
        Task<PaginatedList<Property>> GetAllAsync(int pageNumber, int pageSize);
        Task<Property> UpdateAsync(Property property);
        Task<Property> GetAsync(Expression<Func<Property, bool>> expression);
        Task DeleteAsync(Guid id);
    }
}