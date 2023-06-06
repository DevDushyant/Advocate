using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string UserID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string Description { get; set; }
    }
}
