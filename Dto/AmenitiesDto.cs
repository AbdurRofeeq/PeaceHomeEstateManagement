using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaceHomeEstateManagement.Dto
{
    public class CreateAmenitiesDto
    {
        public string Name { get; set; }
    }

    public class UpdateAmenitiesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class AmenitiesResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<AmenitiesPropertyDto> AmenitiesProperties { get; set; }
    }

    public class AmenitiesPropertyDto
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; } 
    }


}