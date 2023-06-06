using Advocate.Data;
using Advocate.Dtos;
using Advocate.Entities;
using Advocate.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Services
{
    public class BookServiceAsync : GenericServiceAsync<BookEntity>, IBookServiceAsync
    {
        private readonly AdvocateContext advocateContext;
        public BookServiceAsync(AdvocateContext context) : base(context)
        {
            advocateContext = context;
        }

        public IEnumerable<DropDownDto> GetTallyData(string tallytype, string datetype, string date)
        {
            List<DropDownDto> ddlList = new List<DropDownDto>();
            if (tallytype.ToLower() == "Act".ToLower() && datetype == "gazzetedate")
            {
                ddlList = (from act in advocateContext.ActEntities.Where(act => act.GazetteDate.ToString() == date)
                           select new DropDownDto
                           {
                               Id = act.Id,
                               text = act.ActName
                           }).ToList();
            }
            if (tallytype.ToLower() == "Act".ToLower() && datetype == "notification")
            {
                ddlList = (from act in advocateContext.ActEntities.Where(act => act.AssentDate.ToString() == date)
                           select new DropDownDto
                           {
                               Id = act.Id,
                               text = act.ActName
                           }).ToList();
            }

            if (tallytype.ToLower() == "Notification".ToLower() && datetype == "gazzetedate")
            {
                ddlList = (from noti in advocateContext.NotificationEntities.Where(dt => dt.GazetteDate.ToString() == date)
                           select new DropDownDto
                           {
                               Id = noti.Id,
                               text = noti.NotificationNo + " " + noti.Notification_date
                           }).ToList();
            }
            if (tallytype.ToLower() == "Notification".ToLower() && datetype == "notification")
            {
                ddlList = (from noti in advocateContext.NotificationEntities.Where(dt => dt.Notification_date.ToString() == date)
                           select new DropDownDto
                           {
                               Id = noti.Id,
                               text = noti.NotificationNo + " " + noti.Notification_date
                           }).ToList();
            }

            if (tallytype.ToLower() == "Rule".ToLower() && datetype == "gazzetedate")
            {
                ddlList = (from rule in advocateContext.RuleEntities.Where(dt => dt.GazzetDate.ToString() == date)
                           select new DropDownDto
                           {
                               Id = rule.Id,
                               text = rule.RuleName
                           }).ToList();
            }
            if (tallytype.ToLower() == "Rule".ToLower() && datetype == "notification")
            {
                ddlList = (from rule in advocateContext.RuleEntities.Where(dt => dt.RuleDate.ToString() == date)
                           select new DropDownDto
                           {
                               Id = rule.Id,
                               text = rule.RuleName
                           }).ToList();
            }
            return ddlList;
        }

        public bool isActExist(string bookName)
        {
            var data = advocateContext.BookMaster.Where(w => w.IsActive == true && w.BookName.ToLower() == bookName.ToLower());
            if (data.Count() != 0)
                return true;
            else
                return false;
        }

        public int SaveBookEntryDetail(BookEntryDetailEntity bookEntryDetailEntity)
        {
            if (bookEntryDetailEntity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                if (bookEntryDetailEntity.TallyType == "Act") 
                {
                    ActBookEntity adt = new ActBookEntity();
                    adt.ActId = bookEntryDetailEntity.TypeId;
                    adt.BookId = bookEntryDetailEntity.BookId;
                    adt.BookPageNo = bookEntryDetailEntity.BookPageNo;
                    adt.BookSrNo = bookEntryDetailEntity.BookSerialNo;
                    adt.Volume = bookEntryDetailEntity.BookVolume;
                    adt.BookYear = bookEntryDetailEntity.BookYear;
                    adt.IsActive = true;
                    adt.CreatedDate = DateTime.Now;
                    adt.UserID = bookEntryDetailEntity.UserID;
                    advocateContext.ActBookEntities.Add(adt);                    
                }

                if (bookEntryDetailEntity.TallyType == "Notification")
                {
                    NotificationBookEntity adt = new NotificationBookEntity();
                    adt.RuleId = bookEntryDetailEntity.TypeId;
                    adt.BookId = bookEntryDetailEntity.BookId;
                    adt.BookPageNo = bookEntryDetailEntity.BookPageNo;
                    adt.BookSrNo = bookEntryDetailEntity.BookSerialNo;
                    adt.Volume = bookEntryDetailEntity.BookVolume;
                    adt.BookYear = bookEntryDetailEntity.BookYear;
                    adt.IsActive = true;
                    adt.CreatedDate = DateTime.Now;
                    adt.UserID = bookEntryDetailEntity.UserID;
                    advocateContext.NotificationBookEntities.Add(adt);
                }

                if (bookEntryDetailEntity.TallyType == "Rule")
                {
                    RuleBookEntity adt = new RuleBookEntity();
                    adt.RuleId = bookEntryDetailEntity.TypeId;
                    adt.BookId = bookEntryDetailEntity.BookId;
                    adt.BookPageNo = bookEntryDetailEntity.BookPageNo;
                    adt.BookSrNo = bookEntryDetailEntity.BookSerialNo;
                    adt.Volume = bookEntryDetailEntity.BookVolume;
                    adt.BookYear = bookEntryDetailEntity.BookYear;
                    adt.IsActive = true;
                    adt.CreatedDate = DateTime.Now;
                    adt.UserID = bookEntryDetailEntity.UserID;
                    advocateContext.RuleBookEntities.Add(adt);
                }
                return advocateContext.SaveChanges();               
            }
        }

        public BookEntryDetailEntity FindById(int BookDetailid)
        {
            return advocateContext.BookEntryDetailEntities.SingleOrDefault(s => s.Id == BookDetailid);
        }
    }
}
