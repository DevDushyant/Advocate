using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_Gazette")]
    public class GazetteTypeEntity : BaseEntity
    {
        [Column(TypeName ="varchar(150)")]
        public string GazetteName { get; set; }
        public virtual List<PartEntity> PartEntities { get; set; }
    }
}
