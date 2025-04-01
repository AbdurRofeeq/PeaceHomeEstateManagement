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
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IAmenitiesService _amenitiesService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<PropertyController> _logger;

        public PropertyController(IPropertyService propertyService, IPropertyTypeService propertyTypeService, IAmenitiesService amenitiesService,   ICloudinaryService cloudinaryService, ILogger<PropertyController> logger)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _amenitiesService = amenitiesService;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
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

            return View(new CreatePropertyDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create(IFormFile image1File, IFormFile image2File, IFormFile image3File, IFormFile videoFile, CreatePropertyDto property)
        {
            if (await _propertyService.PropertyNameExistsAsync(property.Name))
            {
                ModelState.AddModelError("Name", "A property with the same name already exists.");
                return View(property);
            }
            
            var model = new CreatePropertyDto
            {
                Name = HttpContext.Request.Form["Name"],
                Description = HttpContext.Request.Form["Description"],
                PropertyTypeId = Guid.Parse(HttpContext.Request.Form["PropertyTypeId"]),
                AmenitiesIds = HttpContext.Request.Form["AmenitiesIds"].ToString().Split(',').Select(Guid.Parse).ToList(),
                Address = HttpContext.Request.Form["Address"]
            };

            // Upload files to Cloudinary
            try
            {
                if (image1File != null)
                    model.Image1 = await _cloudinaryService.UploadFileAsync(image1File);

                if (image2File != null)
                    model.Image2 = await _cloudinaryService.UploadFileAsync(image2File);

                if (image3File != null)
                    model.Image3 = await _cloudinaryService.UploadFileAsync(image3File);

                if (videoFile != null)
                    model.Video = await _cloudinaryService.UploadVideoAsync(videoFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload files to Cloudinary.");

                await RepopulateViewBagData();

                ViewBag.Error = "Failed to upload files. Please try again.";
                return View(model);
            }

            var result = await _propertyService.CreateAsync(model);

            if (result != null)
            {
                return RedirectToAction("GetAllProperties");
            }
            else
            {
                await RepopulateViewBagData();

                ViewBag.Error = "Failed to save the property. Please try again.";
                return View(model);
            }
        }

        private async Task RepopulateViewBagData()
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
        }

       public async Task<IActionResult> Edit(Guid id)
        {
            var property = await _propertyService.GetAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            ViewBag.PropertyTypes = await _propertyTypeService.GetAllAsync(); 
 
            ViewBag.Amenities = await _amenitiesService.GetAllAsync(); 

            // Map the property to UpdatePropertyDto
            var updatePropertyDto = new UpdatePropertyDto
            {
                Id = property.Id,
                Name = property.Name,
                Description = property.Description,
                Image1 = property.Image1,
                Image2 = property.Image2,
                Image3 = property.Image3,
                Video = property.Video,
                AmenitiesIds = property.Amenities?.Select(a => a.Id).ToList() ?? new List<Guid>(),
                PropertyTypeId = property.PropertyTypeId 
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