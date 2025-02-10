using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaceHomeEstateManagement.Dto
{
    public class CreatePropertyTypeDto
    {
        public string Name { get; set; }
    }

    public class PropertyTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<PropertyDto> Properties { get; set; } = new List<PropertyDto>();
    }

    public class PropertyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}