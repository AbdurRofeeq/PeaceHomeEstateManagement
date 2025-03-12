using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaceHomeEstateManagement.Dto
{
    public class CreatePropertyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public Guid PropertyTypeId { get; set; }
        public List<Guid> AmenitiesIds { get; set; } = new List<Guid>();

        // Blob Storage URLs
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Video { get; set; }

        // File uploads
        public IFormFile Image1File { get; set; }
        public IFormFile Image2File { get; set; }
        public IFormFile Image3File { get; set; }
        public IFormFile VideoFile { get; set; }
    }

    public class UpdatePropertyDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Video { get; set; }
        public Guid PropertyTypeId { get; set; }
        public IList<Guid> AmenitiesIds { get; set; } 
    }

    public class PropertyResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set;}
        public string Description { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Video { get; set; }
        public string PropertyType { get; set; } 
        public IList<AmenitiesResponseDto> Amenities { get; set; }
    }



}