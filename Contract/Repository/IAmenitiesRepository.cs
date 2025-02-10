using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PeaceHomeEstateManagement.Models;

namespace PeaceHomeEstateManagement.Contract.Repository
{
    public interface IAmenitiesRepository
    {
        Task<Amenities> CreateAsync(Amenities amenities);
        Task<Amenities> GetAsync(Guid id);
        Task<IList<Amenities>> GetAllAsync();
        Task<Amenities> GetAsync(Expression<Func<Amenities, bool>> expression);
        Task<Amenities> UpdateAsync(Amenities amenities);
        Task DeleteAsync(Guid id);
        Task<List<Amenities>> GetListAsync(Expression<Func<Amenities, bool>> expression);
    }
}