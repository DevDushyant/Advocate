using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_NotificationExtAct")]
    public class NotificationExtActEntity:BaseEntity
    {
        public int NotificationId { get; set; }
        public int ActId { get; set; }
    }
}
