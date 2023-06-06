using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_RuleExtAct")]
    public class RuleActExtraEntity:BaseEntity
    {
        public int RuleId { get; set; }
        public int ActId { get; set; }
    }
}
