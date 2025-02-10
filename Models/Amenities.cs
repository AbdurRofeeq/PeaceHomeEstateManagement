using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaceHomeEstateManagement.Models
{
    public class Amenities : BaseEntity
    {
        public string Name {get;set;}
        public IList<AmenitiesProperty> AmenitiesProperties {get;set;}
    }
}