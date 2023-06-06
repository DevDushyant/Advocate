using Advocate.Data;
using Advocate.Dtos;
using Advocate.Entities;
using Advocate.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Services
{
    public class NotificationServiceAsync : GenericServiceAsync<NotificationEntity>, INotificationServiceAsync
    {
        private readonly AdvocateContext advocateContext;
        private readonly IMapper _mapper;
        public NotificationServiceAsync(AdvocateContext context, IMapper _mapper) : base(context)
        {
            advocateContext = context;
            this._mapper = _mapper;
        }

        public int DeleteNotification(int notificationId)
        {
            if (notificationId != 0)
            {
                var notidata = advocateContext.NotificationEntities.SingleOrDefault(n => n.Id == notificationId && n.IsActive == true);
                advocateContext.Remove(notidata);
                int notiDeletedId = advocateContext.SaveChanges();
                if (notiDeletedId != 0)
                {
                    var notiBookdata = advocateContext.NotificationBookEntities.SingleOrDefault(b => b.IsActive == true && b.RuleId == notificationId);
                    if (notiBookdata != null)
                        advocateContext.Remove(notiBookdata);

                    var amdData = advocateContext.NotificationAmendedEntities.SingleOrDefault(b => b.IsActive == true && b.AmendedNotificationID == notificationId);
                    if (amdData != null)
                        advocateContext.Remove(amdData);

                    var rpddata = advocateContext.notificationRepealedEntities.SingleOrDefault(b => b.IsActive == true && b.RepealedNotificationID == notificationId);
                    if (rpddata != null)
                        advocateContext.Remove(rpddata);

                    var notiext = advocateContext.NotificationExtActEntities.SingleOrDefault(b => b.IsActive == true && b.NotificationId == notificationId);
                    if (notiext != null)
                        advocateContext.Remove(notiext);
                }
                int result = advocateContext.SaveChanges();
                return result;
            }
            else
                return 0;
        }

        public IEnumerable<NotificationDto> GetNotification()
        {
            var result = from noti in advocateContext.NotificationEntities.Where(noti => noti.IsActive == true)
                         join notitype in advocateContext.NotificationTypes.Where(type => type.IsActive) on noti.NotificationType equals notitype.Id into notidata
                         from ndata in notidata.DefaultIfEmpty()
                         join rule in advocateContext.RuleEntities.Where(active => active.IsActive == true) on noti.NotificationRuleId equals rule.Id into notirule
                         from rdata in notirule.DefaultIfEmpty()
                         select new NotificationDto
                         {
                             NotificationNo = noti.NotificationNo,
                             NotificationDate = noti.Notification_date.Value.ToString("dd/MM/yyyy"),
                             NotificationType = ndata.Name,
                             Rule = rdata.RuleName,
                             Id = noti.Id
                         };
            return result.ToList();
        }

        public NotificationDetailInfoDto GetNotificationDetailByNotificationId(int notiId)
        {
            var result = from noti in advocateContext.NotificationEntities.Where(n => n.IsActive == true && n.Id == notiId)
                         select new NotificationDetailInfoDto
                         {
                             ActTypeId = noti.ActTypeId,
                             ActId = noti.ActId,
                             NotificationNo = noti.NotificationNo,
                             GSRSO_Prefix = noti.GSR_SO,
                             GSRSO_No = noti.GSRSO_Number,
                             Notification_date = noti.Notification_date,
                             Notifiction_SectionRule = noti.Notifiction_SectionRule,
                             NotificationRuleKind = noti.NotificationRuleKind,
                             NotificationRuleId = noti.NotificationRuleId,
                             GazzetId = noti.GazzetId,
                             PartId = noti.PartId,
                             Nature = noti.Nature,
                             GazetteDate = noti.GazetteDate,
                             NotificationType = noti.NotificationType,
                             PageNo = noti.PageNo,
                             ComeInforce = noti.ComeInforce,
                             PublishedInGazeteDate = noti.PublishedInGazeteDate,
                             Substance = noti.Substance
                         };
            var ruleBook = from rbook in advocateContext.NotificationBookEntities.Where(active => active.IsActive == true && active.RuleId == notiId)
                           select new NotificationBook
                           {
                               RuleId = rbook.RuleId,
                               BookId = rbook.BookId,
                               BookPageNo = rbook.BookPageNo,
                               BookSrNo = rbook.BookSrNo,
                               Volume = rbook.Volume,
                               BookYear = rbook.BookYear
                           };
            var extraActRule = from extRuleAct in advocateContext.NotificationExtActEntities.Where(record => record.IsActive == true && record.NotificationId == notiId)
                               select new NotiExtraAct
                               {
                                   ActId = extRuleAct.ActId
                               };
            var ruleAmended = from notiamd in advocateContext.NotificationAmendedEntities.Where(active => active.IsActive == true && active.AmendedNotificationID == notiId)
                              select new AmendedNotification
                              {
                                  NotificationId = notiamd.NotificationId
                              };
            var ruleRepealed = from rrpld in advocateContext.notificationRepealedEntities.Where(active => active.IsActive == true && active.RepealedNotificationID == notiId)
                               select new RepealedNotification
                               {
                                   NotificationId = rrpld.NotificationId
                               };
            NotificationDetailInfoDto detailInfoDto = new NotificationDetailInfoDto();
            detailInfoDto.notificationBooks = ruleBook.ToList();
            detailInfoDto.ActTypeId = result.Select(r => r.ActTypeId).FirstOrDefault();
            detailInfoDto.ActId = result.Select(r => r.ActId).FirstOrDefault();
            detailInfoDto.NotificationNo = result.Select(r => r.NotificationNo).FirstOrDefault();
            detailInfoDto.GSRSO_Prefix = result.Select(r => r.GSRSO_Prefix).FirstOrDefault();
            detailInfoDto.GSRSO_No = result.Select(r => r.GSRSO_No).FirstOrDefault();
            detailInfoDto.Notification_date = result.Select(r => r.Notification_date).FirstOrDefault();
            detailInfoDto.Notifiction_SectionRule = result.Select(r => r.Notifiction_SectionRule).FirstOrDefault();
            detailInfoDto.NotificationRuleKind = result.Select(r => r.NotificationRuleKind).FirstOrDefault();
            detailInfoDto.NotificationRuleId = result.Select(r => r.NotificationRuleId).FirstOrDefault();
            detailInfoDto.GazzetId = result.Select(r => r.GazzetId).FirstOrDefault();
            detailInfoDto.PartId = result.Select(r => r.PartId).FirstOrDefault();
            detailInfoDto.Nature = result.Select(r => r.Nature).FirstOrDefault();
            detailInfoDto.GazetteDate = result.Select(r => r.GazetteDate).FirstOrDefault();
            detailInfoDto.NotificationType = result.Select(r => r.NotificationType).FirstOrDefault();
            detailInfoDto.PageNo = result.Select(r => r.PageNo).FirstOrDefault();
            detailInfoDto.ComeInforce = result.Select(r => r.ComeInforce).FirstOrDefault();
            detailInfoDto.PublishedInGazeteDate = result.Select(r => r.PublishedInGazeteDate).FirstOrDefault();
            detailInfoDto.Substance = result.Select(r => r.Substance).FirstOrDefault();
            detailInfoDto.ExtraRulesAct = extraActRule.ToList();
            detailInfoDto.RepealedNotification = ruleRepealed.ToList();
            detailInfoDto.AmendedNotification = ruleAmended.ToList();

            return detailInfoDto;
        }

        public int LastInsertedRecord()
        {
            if (advocateContext.NotificationEntities.Count() > 0)
                return advocateContext.NotificationEntities.Where(row => row.IsActive == true).Max(row => row.Id);
            else
                return 0;
        }

        public int SaveorUpdateNotiAmended(List<NotificationAmendedEntity> notiAmendedEntity, int NotiId)
        {
            if (notiAmendedEntity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                if (NotiId != 0)
                {
                    var notiAmnd = advocateContext.NotificationAmendedEntities.Where(ra => ra.NotificationId == NotiId).ToList();
                    advocateContext.NotificationAmendedEntities.RemoveRange(notiAmnd);
                }
                notiAmendedEntity.ForEach(item => advocateContext.NotificationAmendedEntities.Add(item));
                return advocateContext.SaveChanges();
            }
        }

        public int SaveOrUpdateNotiBook(List<NotificationBookEntity> notiBookEntity, int notiId)
        {
            if (notiBookEntity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                if (notiId != 0)
                {
                    var actbook = advocateContext.NotificationBookEntities.Where(rbook => rbook.RuleId == notiId).ToList();
                    advocateContext.NotificationBookEntities.RemoveRange(actbook);
                }
                notiBookEntity.ForEach(item => advocateContext.NotificationBookEntities.Add(item));
                return advocateContext.SaveChanges();
            }
        }

        public int SaveOrUpdateNotiRepealed(List<NotificationRepealedEntity> NotiRepealeds, int notiId)
        {
            if (NotiRepealeds == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                if (notiId != 0)
                {
                    var notiAmnd = advocateContext.notificationRepealedEntities.Where(ra => ra.NotificationId == notiId).ToList();
                    advocateContext.notificationRepealedEntities.RemoveRange(notiAmnd);
                }
                NotiRepealeds.ForEach(item => advocateContext.notificationRepealedEntities.Add(item));
                return advocateContext.SaveChanges();
            }
        }

        public int SaveOrUpdateRuleExtraAct(List<NotificationExtActEntity> extraEntity, int notiId)
        {
            if (extraEntity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                if (notiId != 0)
                {
                    var extRule = advocateContext.NotificationExtActEntities.Where(extRule => extRule.NotificationId == notiId).ToList();
                    advocateContext.NotificationExtActEntities.RemoveRange(extRule);
                }
                extraEntity.ForEach(item => advocateContext.NotificationExtActEntities.Add(item));
                return advocateContext.SaveChanges();
            }
        }
    }
}
