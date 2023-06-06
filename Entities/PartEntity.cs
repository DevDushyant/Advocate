using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_Part")]
    public class PartEntity:BaseEntity
    {
        [Column(TypeName ="varchar(100)")]
        public string PartName { get; set; }

        [ForeignKey("gazetteTypeEntity")]
        public int GazettId { get; set; }
        public GazetteTypeEntity gazetteTypeEntity { get; set; }
    }
}
