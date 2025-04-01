using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeaceHomeEstateManagement.Contract.Service;
using PeaceHomeEstateManagement.Models;

namespace PeaceHomeEstateManagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
     private readonly IPropertyService _propertyService;
    private readonly IPropertyTypeService _propertyTypeService;
    private readonly IAmenitiesService _amenityService;

    public HomeController(ILogger<HomeController> logger, IPropertyService propertyService,
        IPropertyTypeService propertyTypeService,
        IAmenitiesService amenitiesService)
    {
        _logger = logger;
         _propertyService = propertyService;
        _propertyTypeService = propertyTypeService;
        _amenityService = amenitiesService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> DashBoard(int pageNumber = 1, int pageSize = 10)
    {
        var properties = await _propertyService.GetAllAsync(pageNumber, pageSize);
        ViewBag.Properties = properties;

        var propertyTypes = await _propertyTypeService.GetAllAsync();
        ViewBag.PropertyTypes = propertyTypes;

        var amenities = await _amenityService.GetAllAsync();
        ViewBag.Amenities = amenities;

        return View();
    }
}
