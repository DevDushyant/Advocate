using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_Notification")]
    public class NotificationEntity : BaseEntity
    {
        public int ActTypeId { get; set; }
        public int ActId { get; set; }
        public string  NotificationNo { get; set; }
        public string  GSR_SO { get; set; }
        public string  GSRSO_Number { get; set; }
        public DateTime?  Notification_date { get; set; }
        public string Notifiction_SectionRule { get; set; }
        public string NotificationRuleKind { get; set; }
        public int NotificationRuleId { get; set; }
        public int GazzetId { get; set; }
        public int PartId { get; set; }
        public string Nature { get; set; }
        public DateTime? GazetteDate { get; set; }
        public int NotificationType { get; set; }
        public int? PageNo { get; set; }
        public string ComeInforce { get; set; }
        public DateTime? PublishedInGazeteDate { get; set; }
        public string Substance { get; set; }
    }
}
