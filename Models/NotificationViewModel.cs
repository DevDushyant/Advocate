using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Models
{
    public class NotificationViewModel : BaseViewModel
    {
        public int ActTypeId { get; set; }
        public int ActId { get; set; }
        public string NotificationNo { get; set; }
        public string GSRSO_Prefix { get; set; }
        public string GSRSO_No { get; set; }
        public DateTime? Notification_date { get; set; }
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
        public string Substance { get; set; }

        public DateTime? ComeInforceEFDate { get; set; }
        public DateTime? PublishedInGazeteDate { get; set; }
        public List<NotificationBook> notificationBooks { get; set; }
        public string AmendedRules { get; set; }
        public string RepealedRules { get; set; }
        public string ExtraRulesAct { get; set; }

    }
    public class NotificationBook
    {
        public int RuleId { get; set; }
        public int BookId { get; set; }
        public int BookYear { get; set; }
        public string BookPageNo { get; set; }
        public int? BookSrNo { get; set; }
        public string Volume { get; set; }
        public int MyProperty { get; set; }
    }

}
