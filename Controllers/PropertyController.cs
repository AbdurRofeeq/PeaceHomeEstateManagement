using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PeaceHomeEstateManagement.Contract.Service;
using PeaceHomeEstateManagement.Dto;
using PeaceHomeEstateManagement.Implementation.Service;

namespace PeaceHomeEstateManagement.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IAmenitiesService _amenitiesService;

        public PropertyController(IPropertyService propertyService, IBlobStorageService blobStorageService, IPropertyTypeService propertyTypeService, IAmenitiesService amenitiesService)
        {
            _propertyService = propertyService;
            _blobStorageService = blobStorageService;
            _propertyTypeService = propertyTypeService;
            _amenitiesService = amenitiesService;
        }

        public async Task<IActionResult> Create()
        {
            var propertyTypes = await _propertyTypeService.GetAllAsync();
            ViewBag.PropertyTypes = propertyTypes.Select(pt => new SelectListItem
            {
                Value = pt.Id.ToString(),
                Text = pt.Name
            });

            var amenities = await _amenitiesService.GetAllAsync();
            ViewBag.Amenities = amenities.Select(am => new SelectListItem
            {
                Value = am.Id.ToString(),
                Text = am.Name
            });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyDto createPropertyDto)
        {
              var form = HttpContext.Request.Form;

                var createPropertyDt = new CreatePropertyDto
                {
                    Name = form["Name"],
                    Description = form["Description"],
                    Address = form["Address"],
                    PropertyTypeId = Guid.Parse(form["PropertyTypeId"]),
                    AmenitiesIds = form["AmenitiesIds"].ToString().Split(',').Select(Guid.Parse).ToList(),
                    Image1File = form.Files["Image1File"],
                    Image2File = form.Files["Image2File"],
                    Image3File = form.Files["Image3File"],
                    VideoFile = form.Files["VideoFile"]
                };

            try
            {
                // Upload images and video to Blob Storage
                createPropertyDto.Image1 = createPropertyDto.Image1File != null
                    ? await _blobStorageService.UploadAsync(createPropertyDto.Image1File)
                    : null;

                createPropertyDto.Image2 = createPropertyDto.Image2File != null
                    ? await _blobStorageService.UploadAsync(createPropertyDto.Image2File)
                    : null;

                createPropertyDto.Image3 = createPropertyDto.Image3File != null
                    ? await _blobStorageService.UploadAsync(createPropertyDto.Image3File)
                    : null;

                createPropertyDto.Video = createPropertyDto.VideoFile != null
                    ? await _blobStorageService.UploadAsync(createPropertyDto.VideoFile)
                    : null;

                // Save property to the database
                var propertyResponse = await _propertyService.CreateAsync(createPropertyDto);
                return RedirectToAction(nameof(GetAllProperties));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating property. Please try again.");
                return View(createPropertyDto);
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var property = await _propertyService.GetAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            var updatePropertyDto = new UpdatePropertyDto
            {
                Id = property.Id,
                Name = property.Name,
                Description = property.Description,
                Image1 = property.Image1,
                Image2 = property.Image2,
                Image3 = property.Image3,
                Video = property.Video,
                AmenitiesIds = property.Amenities.Select(a => a.Id).ToList()
            };

            return View(updatePropertyDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePropertyDto updatePropertyDto)
        {
            try
            {
                var updatedProperty = await _propertyService.UpdateAsync(updatePropertyDto);
                if (updatedProperty == null)
                {
                    return NotFound();
                }

                return RedirectToAction("Details", new { id = updatedProperty.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating property. Please try again.");
                return View(updatePropertyDto);
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var property = await _propertyService.GetAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var property = await _propertyService.GetAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            try
            {
                await _propertyService.DeleteAsync(id);

                return RedirectToAction("GetAllProperties");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error deleting property. Please try again.");
                return View();
            }
        }
 
        public async Task<IActionResult> GetAllProperties(int pageNumber = 1, int pageSize = 10)
        {
            var properties = await _propertyService.GetAllAsync(pageNumber, pageSize);
            return View(properties);
        }
    }
}