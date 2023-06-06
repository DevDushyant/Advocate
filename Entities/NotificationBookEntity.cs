using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_NotificationBook")]
    public class NotificationBookEntity : BaseEntity
    {
        [ForeignKey("Rule")]
        public int RuleId { get; set; }
        public int BookId { get; set; }
        public int BookYear { get; set; }
        public string BookPageNo { get; set; }
        public int? BookSrNo { get; set; }
        public string Volume { get; set; }
    }
}
