using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_ActTypes")]
    public class ActTypeEntity:BaseEntity
    {
        [Column(TypeName ="varchar(100)")]
        public string ActType { get; set; }
    }
}
