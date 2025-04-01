using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeaceHomeEstateManagement.Contract.Service;
using PeaceHomeEstateManagement.Dto;

namespace PeaceHomeEstateManagement.Controllers
{
    public class AmenitiesController : Controller
    {
        private readonly IAmenitiesService _amenitiesService;

        public AmenitiesController(IAmenitiesService amenitiesService)
        {
            _amenitiesService = amenitiesService;
        }

        public async Task<IActionResult> GetAllAmenities()
        {
            var amenitiesList = await _amenitiesService.GetAllAsync();
            return View(amenitiesList); 
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var amenities = await _amenitiesService.GetAsync(id);
            if (amenities == null)
            {
                return NotFound(); 
            }

            return View(amenities); 
        }

        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAmenitiesDto createAmenitiesDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createAmenitiesDto); 
            }

            await _amenitiesService.CreateAsync(createAmenitiesDto);
            return RedirectToAction(nameof(GetAllAmenities)); 
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var amenities = await _amenitiesService.GetAsync(id);
            if (amenities == null)
            {
                return NotFound();
            }

            var updateAmenitiesDto = new UpdateAmenitiesDto
            {
                Id = amenities.Id,
                Name = amenities.Name ?? string.Empty
            };

            return View(updateAmenitiesDto); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateAmenitiesDto updateAmenitiesDto)
        {
            var updatedAmenities = await _amenitiesService.UpdateAsync(updateAmenitiesDto);
            if (updatedAmenities == null)
            {
                return NotFound();  
            }

            return RedirectToAction(nameof(GetAllAmenities));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var amenities = await _amenitiesService.GetAsync(id);
            if (amenities == null)
            {
                return NotFound();
            }

            return View(amenities);  
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _amenitiesService.DeleteAsync(id);
            return RedirectToAction(nameof(GetAllAmenities));
        }
    }
}