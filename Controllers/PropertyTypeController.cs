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
    public class PropertyTypeController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;

        public PropertyTypeController(IPropertyTypeService propertyTypeService)
        {
            _propertyTypeService = propertyTypeService;
        }

        public async Task<IActionResult> GetAllPropertyTypes()
        {
            var propertyTypes = await _propertyTypeService.GetAllAsync();
            return View(propertyTypes); 
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var propertyType = await _propertyTypeService.GetAsync(id);
            if (propertyType == null)
            {
                return NotFound();  
            }

            return View(propertyType); 
        }

        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyTypeDto createPropertyTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createPropertyTypeDto); 
            }

            try
            {
                var propertyTypeResponse = await _propertyTypeService.CreateAsync(createPropertyTypeDto);
                return RedirectToAction(nameof(GetAllPropertyTypes));
                //return RedirectToAction("Create", "Property", new { propertyTypeId = propertyTypeResponse.Id });  
            }
            catch (InvalidOperationException ex)
            {
                // Add the exception message to ModelState
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (Exception ex)
            {
                // General error handling for other unexpected exceptions
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
            }

            return View(createPropertyTypeDto); 
        }


        // [HttpPost]
        // public async Task<IActionResult> Create(CreatePropertyTypeDto createPropertyTypeDto)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View(createPropertyTypeDto); 
        //     }

        //     var propertyTypeResponse = await _propertyTypeService.CreateAsync(createPropertyTypeDto);
        //     return RedirectToAction("Create", "Property", new {propertyTypeId = propertyTypeResponse.Id});  
        // }

        public async Task<IActionResult> Edit(Guid id)
        {
            var propertyType = await _propertyTypeService.GetAsync(id);
            if (propertyType == null)
            {
                return NotFound();  
            }

            var updatePropertyTypeDto = new CreatePropertyTypeDto
            {
                Name = propertyType.Name
            };

            return View(updatePropertyTypeDto);  
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CreatePropertyTypeDto updatePropertyTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updatePropertyTypeDto);  
            }

            try
            {
                await _propertyTypeService.UpdateAsync(id, updatePropertyTypeDto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(updatePropertyTypeDto);
            }

            return RedirectToAction(nameof(GetAllPropertyTypes));  
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var propertyType = await _propertyTypeService.GetAsync(id);
            if (propertyType == null)
            {
                return NotFound(); 
            }
            return View(propertyType); 
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _propertyTypeService.DeleteAsync(id);
            return RedirectToAction(nameof(GetAllPropertyTypes));  
        }
    }
}
