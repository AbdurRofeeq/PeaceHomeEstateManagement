using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PeaceHomeEstateManagement.Models
{
    public abstract class BaseEntity
    {
        [Key]
        [Column(TypeName = "char(36)")]
        public Guid Id {get;set;} = Guid.NewGuid();
        public bool IsDeleted {get;set;}
    }
}