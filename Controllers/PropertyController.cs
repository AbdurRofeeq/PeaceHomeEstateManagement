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
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<PropertyController> _logger;

        public PropertyController(IPropertyService propertyService, IBlobStorageService blobStorageService, IPropertyTypeService propertyTypeService, IAmenitiesService amenitiesService,   ICloudinaryService cloudinaryService, ILogger<PropertyController> logger)
        {
            _propertyService = propertyService;
            _blobStorageService = blobStorageService;
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


        // [HttpPost]
        // public async Task<IActionResult> Create(CreatePropertyDto createPropertyDto)
        // {
        //       var form = HttpContext.Request.Form;

        //         var createPropertyDt = new CreatePropertyDto
        //         {
        //             Name = form["Name"],
        //             Description = form["Description"],
        //             Address = form["Address"],
        //             PropertyTypeId = Guid.Parse(form["PropertyTypeId"]),
        //             AmenitiesIds = form["AmenitiesIds"].ToString().Split(',').Select(Guid.Parse).ToList(),
        //             Image1File = form.Files["Image1File"],
        //             Image2File = form.Files["Image2File"],
        //             Image3File = form.Files["Image3File"],
        //             VideoFile = form.Files["VideoFile"]
        //         };
            
        //     try
        //     {
        //         // Upload images and video to Blob Storage
        //         createPropertyDto.Image1 = createPropertyDto.Image1File != null
        //             ? await _blobStorageService.UploadAsync(createPropertyDto.Image1File)
        //             : null;

        //         createPropertyDto.Image2 = createPropertyDto.Image2File != null
        //             ? await _blobStorageService.UploadAsync(createPropertyDto.Image2File)
        //             : null;

        //         createPropertyDto.Image3 = createPropertyDto.Image3File != null
        //             ? await _blobStorageService.UploadAsync(createPropertyDto.Image3File)
        //             : null;

        //         createPropertyDto.Video = createPropertyDto.VideoFile != null
        //             ? await _blobStorageService.UploadAsync(createPropertyDto.VideoFile)
        //             : null;

        //         // Save property to the database
        //         var propertyResponse = await _propertyService.CreateAsync(createPropertyDto);
        //         return RedirectToAction(nameof(GetAllProperties));
        //     }
        //     catch (Exception ex)
        //     {
        //         ModelState.AddModelError("", "Error creating property. Please try again.");
        //         return View(createPropertyDto);
        //     }
        // }

        // [HttpPost]
        // public async Task<IActionResult> Create(CreatePropertyDto createPropertyDto)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         // Repopulate ViewBag data and return the view with validation errors
        //         var propertyTypes = await _propertyTypeService.GetAllAsync();
        //         ViewBag.PropertyTypes = propertyTypes.Select(pt => new SelectListItem
        //         {
        //             Value = pt.Id.ToString(),
        //             Text = pt.Name
        //         });

        //         var amenities = await _amenitiesService.GetAllAsync();
        //         ViewBag.Amenities = amenities.Select(am => new SelectListItem
        //         {
        //             Value = am.Id.ToString(),
        //             Text = am.Name
        //         });

        //         return View(createPropertyDto);
        //     }

        //     try
        //     {
        //         // Upload images and video to Blob Storage
        //         createPropertyDto.Image1 = createPropertyDto.Image1File != null
        //             ? await _blobStorageService.UploadAsync(createPropertyDto.Image1File)
        //             : null;

        //         createPropertyDto.Image2 = createPropertyDto.Image2File != null
        //             ? await _blobStorageService.UploadAsync(createPropertyDto.Image2File)
        //             : null;

        //         createPropertyDto.Image3 = createPropertyDto.Image3File != null
        //             ? await _blobStorageService.UploadAsync(createPropertyDto.Image3File)
        //             : null;

        //         createPropertyDto.Video = createPropertyDto.VideoFile != null
        //             ? await _blobStorageService.UploadAsync(createPropertyDto.VideoFile)
        //             : null;

        //         // Save property to the database
        //         var propertyResponse = await _propertyService.CreateAsync(createPropertyDto);
        //         return RedirectToAction(nameof(GetAllProperties));
        //     }
        //     catch (Exception ex)
        //     {
        //         ModelState.AddModelError("", "Error creating property. Please try again.");

        //         // Repopulate ViewBag data and return the view with the error
        //         var propertyTypes = await _propertyTypeService.GetAllAsync();
        //         ViewBag.PropertyTypes = propertyTypes.Select(pt => new SelectListItem
        //         {
        //             Value = pt.Id.ToString(),
        //             Text = pt.Name
        //         });

        //         var amenities = await _amenitiesService.GetAllAsync();
        //         ViewBag.Amenities = amenities.Select(am => new SelectListItem
        //         {
        //             Value = am.Id.ToString(),
        //             Text = am.Name
        //         });

        //         return View(createPropertyDto);
        //     }
        // }

        // [HttpPost]
        // public async Task<IActionResult> Create(CreatePropertyDto createPropertyDto)
        // {
        //     // if (!ModelState.IsValid)
        //     // {
        //     //     // Repopulate ViewBag data and return the view with validation errors
        //     //     await RepopulateViewBagDataAsync();
        //     //     return View(createPropertyDto);
        //     // }

        //     try
        //     {
        //         // Upload Image1 and update the string field
        //         if (createPropertyDto.Image1File != null)
        //         {
        //             createPropertyDto.Image1 = await _blobStorageService.UploadAsync(createPropertyDto.Image1File);
        //         }

        //         // Upload Image2 and update the string field
        //         if (createPropertyDto.Image2File != null)
        //         {
        //             createPropertyDto.Image2 = await _blobStorageService.UploadAsync(createPropertyDto.Image2File);
        //         }

        //         // Upload Image3 and update the string field
        //         if (createPropertyDto.Image3File != null)
        //         {
        //             createPropertyDto.Image3 = await _blobStorageService.UploadAsync(createPropertyDto.Image3File);
        //         }

        //         // Upload Video and update the string field
        //         if (createPropertyDto.VideoFile != null)
        //         {
        //             createPropertyDto.Video = await _blobStorageService.UploadAsync(createPropertyDto.VideoFile);
        //         }

        //         // Save property to the database
        //         var propertyResponse = await _propertyService.CreateAsync(createPropertyDto);
        //         return RedirectToAction(nameof(GetAllProperties));
        //     }
        //     catch (Exception ex)
        //     {
        //         ModelState.AddModelError("", "Error creating property. Please try again.");

        //         // Repopulate ViewBag data and return the view with the error
        //         await RepopulateViewBagDataAsync();
        //         return View(createPropertyDto);
        //     }
        // }

        // [HttpPost]
        // public async Task<IActionResult> Create(IFormFile image1File, IFormFile image2File, IFormFile image3File, IFormFile videoFile)
        // {
        //     var model = new CreatePropertyDto();
        //     if (ModelState.IsValid)
        //     {
        //         // Upload files to Cloudinary
        //         if (image1File != null)
        //             model.Image1 = await _cloudinaryService.UploadFileAsync(image1File);

        //         if (image2File != null)
        //             model.Image2 = await _cloudinaryService.UploadFileAsync(image2File);

        //         if (image3File != null)
        //             model.Image3 = await _cloudinaryService.UploadFileAsync(image3File);

        //         if (videoFile != null)
        //             model.Video = await _cloudinaryService.UploadVideoAsync(videoFile);

        //         // Save the model to the database (e.g., using Entity Framework)
        //         // _context.Properties.Add(model);
        //          await _propertyService.CreateAsync(model);

        //         return RedirectToAction("GetAllProperties");
        //     }

        //     // Repopulate ViewBag data
        //     var propertyTypes = await _propertyTypeService.GetAllAsync();
        //     ViewBag.PropertyTypes = propertyTypes.Select(pt => new SelectListItem
        //     {
        //         Value = pt.Id.ToString(),
        //         Text = pt.Name
        //     });

        //     var amenities = await _amenitiesService.GetAllAsync();
        //     ViewBag.Amenities = amenities.Select(am => new SelectListItem
        //     {
        //         Value = am.Id.ToString(),
        //         Text = am.Name
        //     });

        //     // Return the view with the model
        //     return View(model);
        // }


        [HttpPost]
        public async Task<IActionResult> Create(IFormFile image1File, IFormFile image2File, IFormFile image3File, IFormFile videoFile)
        {
            // Manually bind form data to the model
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
                // Log the error (e.g., using ILogger)
                _logger.LogError(ex, "Failed to upload files to Cloudinary.");

                // Repopulate ViewBag data
                await RepopulateViewBagData();

                // Return the view with the model and an error message
                ViewBag.Error = "Failed to upload files. Please try again.";
                return View(model);
            }

            // Save the model to the database (e.g., using Entity Framework)
            var result = await _propertyService.CreateAsync(model);

            if (result != null)
            {
                return RedirectToAction("GetAllProperties");
            }
            else
            {
                // Repopulate ViewBag data
                await RepopulateViewBagData();

                // Return the view with the model and an error message
                ViewBag.Error = "Failed to save the property. Please try again.";
                return View(model);
            }
        }

// Helper method to repopulate ViewBag data
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