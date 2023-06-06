using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_RepealedRule")]
    public class RuleRepealedEntity : BaseEntity
    {
        [ForeignKey("Rule")]
        public int RuleId { get; set; }
        public int RepealedRuleID { get; set; }
    }
}
