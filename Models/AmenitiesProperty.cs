using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaceHomeEstateManagement.Models
{
    public class AmenitiesProperty : BaseEntity
    {
        public Guid PropertyId {get;set;}
        public Property Property {get;set;}
        public Guid AmenitiesId {get;set;}
        public Amenities Amenities {get;set;}

    }
}