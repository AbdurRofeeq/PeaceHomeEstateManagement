using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaceHomeEstateManagement.Models
{
    public class PropertyType: BaseEntity
    {
        public string Name {get;set;}
        public IList<Property> Properties {get;set;} = new List<Property>();
    }
}