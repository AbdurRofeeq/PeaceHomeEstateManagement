using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeaceHomeEstateManagement.Context;
using PeaceHomeEstateManagement.Contract.Repository;
using PeaceHomeEstateManagement.Models;
using PeaceHomeEstateManagement.Paging;

namespace PeaceHomeEstateManagement.Implementation.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationContext _context;

        public PropertyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Property> CreateAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync(); 
            return property;
        }

        public async Task<PaginatedList<Property>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Properties
                .Include(p => p.PropertyType) 
                .Include(p => p.AmenitiesProperties)
                    .ThenInclude(ap => ap.Amenities)
                .Where(p => !p.IsDeleted);

            return new PaginatedList<Property>
            {
                Items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(),
                TotalItems = await query.CountAsync(),
                Page = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Property?> GetAsync(Guid id)
        {
            return await _context.Properties
                .Include(p => p.PropertyType) 
                .Include(p => p.AmenitiesProperties)
                    .ThenInclude(ap => ap.Amenities) 
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<Property?> GetAsync(Expression<Func<Property, bool>> expression)
        {
            return await _context.Properties
                .Include(p => p.PropertyType) 
                .Include(p => p.AmenitiesProperties)
                    .ThenInclude(ap => ap.Amenities) 
                .FirstOrDefaultAsync(expression);
        }

        public async Task<Property> UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync(); 
            return property;
        }

        public async Task DeleteAsync(Guid id)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(p => p.Id == id);
            if (property != null)
            {
                property.IsDeleted = true; 
                _context.Properties.Update(property); 
                await _context.SaveChangesAsync(); 
            }
        }

    }

}
