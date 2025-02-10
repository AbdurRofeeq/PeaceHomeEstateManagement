using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeaceHomeEstateManagement.Context;
using PeaceHomeEstateManagement.Contract.Repository;
using PeaceHomeEstateManagement.Models;

namespace PeaceHomeEstateManagement.Implementation.Repository
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly ApplicationContext _context;
        public PropertyTypeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PropertyType> CreateAsync(PropertyType propertyType)
        {
            await _context.PropertyTypes.AddAsync(propertyType);
            await _context.SaveChangesAsync(); 
            return propertyType;
        }

        public async Task<IList<PropertyType>> GetAllAsync()
        {
            return await _context.PropertyTypes
                .Include(pt => pt.Properties)
                .Where(pt => !pt.IsDeleted) 
                .ToListAsync();
        }

        public async Task<PropertyType?> GetAsync(Guid id)
        {
            return await _context.PropertyTypes
                .Include(pt => pt.Properties)
                .FirstOrDefaultAsync(pt => pt.Id == id && !pt.IsDeleted);
        }

        public async Task<PropertyType?> GetAsync(Expression<Func<PropertyType, bool>> expression)
        {
            return await _context.PropertyTypes
                .Include(pt => pt.Properties)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<PropertyType> UpdateAsync(PropertyType propertyType)
        {
            _context.PropertyTypes.Update(propertyType);
            await _context.SaveChangesAsync(); 
            return propertyType;
        }

        public async Task DeleteAsync(Guid id)
        {
            var propertyType = await _context.PropertyTypes.FirstOrDefaultAsync(pt => pt.Id == id);
            if (propertyType != null)
            {
                propertyType.IsDeleted = true;
                _context.PropertyTypes.Update(propertyType);
                await _context.SaveChangesAsync(); 
            }
        }

        public async Task<PropertyType?> GetByNameAsync(string name)
        {
            return await _context.PropertyTypes.FirstOrDefaultAsync(pt => pt.Name == name);
        }

    }

}