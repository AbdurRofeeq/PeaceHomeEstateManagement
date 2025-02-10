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
    public class AmenitiesRepository : IAmenitiesRepository
    {
        private readonly ApplicationContext _context;

        public AmenitiesRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Amenities> CreateAsync(Amenities amenities)
        {
            await _context.Amenities.AddAsync(amenities);
            await _context.SaveChangesAsync(); 
            return amenities;
        }

        public async Task<IList<Amenities>> GetAllAsync()
        {
            return await _context.Amenities
                .Include(a => a.AmenitiesProperties)
                .Where(a => !a.IsDeleted)
                .ToListAsync();
        }

        public async Task<Amenities?> GetAsync(Guid id)
        {
            return await _context.Amenities
                .Include(a => a.AmenitiesProperties)
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<Amenities?> GetAsync(Expression<Func<Amenities, bool>> expression)
        {
            return await _context.Amenities
                .Include(a => a.AmenitiesProperties)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<Amenities> UpdateAsync(Amenities amenities)
        {
            _context.Amenities.Update(amenities);
            await _context.SaveChangesAsync(); 
            return amenities;
        }

        public async Task DeleteAsync(Guid id)
        {
            var amenities = await _context.Amenities.FirstOrDefaultAsync(a => a.Id == id);
            if (amenities != null)
            {
                amenities.IsDeleted = true;
                _context.Amenities.Update(amenities);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Amenities>> GetListAsync(Expression<Func<Amenities, bool>> expression)
        {
            return await _context.Amenities
                                .Include(a => a.AmenitiesProperties) 
                                .Where(expression)
                                .ToListAsync();
        }
    }
}