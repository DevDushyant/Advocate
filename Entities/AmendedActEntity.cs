using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_AmendedAct")]
    public class AmendedActEntity : BaseEntity
    {
        [ForeignKey("Act")]
        public int ActID { get; set; }       
        public int AmendedActID { get; set; }
        public ActEntity Act { get; set; }
    }
}
