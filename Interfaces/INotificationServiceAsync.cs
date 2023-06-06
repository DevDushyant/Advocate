using Advocate.Dtos;
using Advocate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Interfaces
{
   public interface INotificationServiceAsync : IGenericServiceAsync<NotificationEntity>
    {
        IEnumerable<NotificationDto> GetNotification();
        //IEnumerable<RuleDto> GetRuleByActId(int actId,string ruleKind);
        NotificationDetailInfoDto GetNotificationDetailByNotificationId(int notiId);
        int LastInsertedRecord();
        int SaveOrUpdateRuleExtraAct(List<NotificationExtActEntity> extraEntity, int ruleId);
        int SaveOrUpdateNotiBook(List<NotificationBookEntity> ruleBookEntity, int ruleId);
        int SaveorUpdateNotiAmended(List<NotificationAmendedEntity> ruleAmendedEntity, int ruleId);
        int SaveOrUpdateNotiRepealed(List<NotificationRepealedEntity> ruleRepealeds, int ruleId);

        //int DeleteRuleBook(RuleBookEntity ruleBookEntity);
        //int DeleteAmendedRule(RuleAmendedEntity ruleAmendedEntity);
        //int DeleteRepealedRule(RuleRepealedEntity repealedEntity);
        //int DeleteExtraAct(RuleActExtraEntity extraEntity);

        int DeleteNotification( int notificationId);
    }
}
