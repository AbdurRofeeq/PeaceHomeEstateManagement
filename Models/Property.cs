using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaceHomeEstateManagement.Models
{
    public class Property : BaseEntity
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public string Image1 {get;set;}
        public string Image2 {get;set;}
        public string Image3 {get;set;}
        public string Video {get;set;}
        public string Address {get;set;}
        public Guid PropertyTypeId {get;set;}
        public PropertyType PropertyType {get;set;}
        public IList<AmenitiesProperty> AmenitiesProperties {get;set;}
    }
}