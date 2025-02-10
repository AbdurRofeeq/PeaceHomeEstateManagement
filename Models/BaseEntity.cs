using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaceHomeEstateManagement.Models
{
    public abstract class BaseEntity
    {
        public Guid Id {get;set;} = Guid.NewGuid();
        public bool IsDeleted {get;set;}
    }
}