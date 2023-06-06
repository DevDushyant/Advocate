using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_Subjects")]
    public class SubjectEntity : BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
    }
}
