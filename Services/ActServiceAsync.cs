using Advocate.Data;
using Advocate.Dtos;
using Advocate.Entities;
using Advocate.Interfaces;
using Advocate.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Services
{
    public class ActServiceAsync : GenericServiceAsync<ActEntity>, IActServiceAsync
    {
        private readonly AdvocateContext advocateContext;
        private readonly IMapper _mapper;
        public ActServiceAsync(AdvocateContext context, IMapper _mapper) : base(context)
        {
            advocateContext = context;
            this._mapper = _mapper;
        }

        public IEnumerable<ActDto> GetAllActs()
        {
            var data = from act in advocateContext.ActEntities.Where(active => active.IsActive == true)
                       join acttype in advocateContext.ActTyes on act.ActTypeId equals acttype.Id
                       where acttype.IsActive == true
                       select new ActDto
                       {
                           ActName = act.ActName,
                           ActYear = act.ActYear,
                           ActNumber = act.ActNumber,
                           ActCategory = act.ActCategory,
                           Id = act.Id,
                           ActTypeId = acttype.Id,
                           ActType = acttype.ActType
                       };
            return data.ToList().OrderBy(o => o.ActNumber);
        }

        public int SaveAmendedAct(List<AmendedActEntity> selectedActId)
        {
            if (selectedActId == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                selectedActId.ForEach(item => advocateContext.AmendedActEntities.Add(item));
                return advocateContext.SaveChanges();
            }
        }

        public int SaveRepealedAct(List<RepealedActEntity> selectedActId)
        {
            if (selectedActId == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                selectedActId.ForEach(item => advocateContext.RepealedActEntities.Add(item));
                return advocateContext.SaveChanges();
            }
        }

        public int SaveActBook(List<ActBookEntity> actBookEntities)
        {
            if (actBookEntities == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                actBookEntities.ForEach(item => advocateContext.ActBookEntities.Add(item));
                return advocateContext.SaveChanges();
            }
        }

        public ActEntity GetActDetailByActId(string userid, int ActId)
        {
            var data = (
                                from actdata in advocateContext.ActEntities.Where(act => act.Id == ActId && act.IsActive == true)
                                join amd in advocateContext.AmendedActEntities.Where(w => w.IsActive == true) on actdata.Id equals amd.AmendedActID into amendedAct
                                from amct in amendedAct.DefaultIfEmpty()

                                join rpt in advocateContext.RepealedActEntities.Where(w => w.IsActive == true) on actdata.Id equals rpt.RepealedActID into rptAct
                                from rpl in rptAct.DefaultIfEmpty()

                                join book in advocateContext.ActBookEntities.Where(w => w.IsActive == true) on actdata.Id equals book.ActId into ActBook
                                from acb in ActBook.DefaultIfEmpty()
                                select new ActEntity
                                {
                                    ActTypeId = actdata.ActTypeId,
                                    ActNumber = actdata.ActNumber,
                                    ActYear = actdata.ActYear,
                                    AssentBy = actdata.AssentBy,
                                    AssentDate = actdata.AssentDate,
                                    ActName = actdata.ActName,
                                    GazetteId = actdata.GazetteId,
                                    PartId = actdata.PartId,
                                    Nature = actdata.Nature,
                                    GazetteDate = actdata.GazetteDate,
                                    PageNo = actdata.PageNo,
                                    ComeInforce = actdata.ComeInforce,
                                    SubjectAct = actdata.SubjectAct,
                                    ActCategory = actdata.ActCategory,
                                    ActBookList = new List<ActBookEntity>()
                                    {
                                            new ActBookEntity()
                                            {
                                                BookId=acb.BookId,
                                                BookSrNo=acb.BookSrNo,
                                                BookYear=acb.BookYear,
                                                Volume=acb.Volume,
                                                BookPageNo=acb.BookPageNo
                                            }
                                    },
                                    AmendedActList = new List<AmendedActEntity>()
                                    {
                                            new AmendedActEntity()
                                            {
                                                ActID=amct.ActID,
                                                AmendedActID=amct.AmendedActID
                                            }
                                    },
                                    RepealedActList = new List<RepealedActEntity>()
                                    {
                                            new RepealedActEntity()
                                            {
                                                ActID=rpl.ActID,
                                                RepealedActID=rpl.RepealedActID
                                            }
                                    }
                                }
                        );
            ActEntity actEntity = new ActEntity();
            actEntity = data.FirstOrDefault();
            List<AmendedActEntity> amendedActEntity = new List<AmendedActEntity>();
            List<RepealedActEntity> repealdActEntity = new List<RepealedActEntity>();
            List<ActBookEntity> bookActEntity = new List<ActBookEntity>();
            foreach (var item in data)
            {
                AmendedActEntity amendedAct = new AmendedActEntity();
                amendedAct.ActID = item.AmendedActList.Select(it => it.ActID).FirstOrDefault();
                amendedAct.AmendedActID = item.AmendedActList.Select(it => it.AmendedActID).FirstOrDefault();
                amendedActEntity.Add(amendedAct);

                RepealedActEntity replact = new RepealedActEntity();
                replact.ActID = item.RepealedActList.Select(it => it.ActID).FirstOrDefault();
                replact.RepealedActID = item.RepealedActList.Select(it => it.RepealedActID).FirstOrDefault();
                repealdActEntity.Add(replact);

                ActBookEntity sactbook = new ActBookEntity();
                sactbook.BookPageNo = item.ActBookList.Select(it => it.BookPageNo).FirstOrDefault();
                sactbook.BookYear = item.ActBookList.Select(it => it.BookYear).FirstOrDefault();
                sactbook.BookSrNo = item.ActBookList.Select(it => it.BookSrNo).FirstOrDefault();
                sactbook.BookId = item.ActBookList.Select(it => it.BookId).FirstOrDefault();
                sactbook.Volume = item.ActBookList.Select(it => it.Volume).FirstOrDefault();

                bookActEntity.Add(sactbook);
            }
            actEntity.AmendedActList = amendedActEntity.GroupBy(s => s.ActID).Select(g => g.FirstOrDefault()).ToList();
            actEntity.RepealedActList = repealdActEntity.GroupBy(a => a.ActID).Select(g => g.FirstOrDefault()).ToList();
            actEntity.ActBookList = bookActEntity.GroupBy(a => a.BookId).Select(g => g.FirstOrDefault()).ToList();

            return actEntity;


        }

        public ActEntity LastInsertedData(string userid)
        {
            int lastRowId = advocateContext.ActEntities.Max(row => row.Id);
            return GetActDetailByActId(userid, lastRowId);
        }

        public int GetActIdByActType_Number_year(int TypeId, int ActNumber, int Year)
        {
            int actId = advocateContext.ActEntities
                            .Where(act => act.ActTypeId == TypeId && act.ActNumber == ActNumber && act.ActYear == Year && act.IsActive == true)
                            .Select(act => act.Id).FirstOrDefault();
            return actId;
        }

        public List<ActRepealedInfoDto> GetActInfoByRepealedAct(int actId)
        {
            var repealedInfo = advocateContext.RepealedActEntities.Where(act => act.ActID == actId).Select(rpa => rpa.RepealedActID).ToList();

            var data = from act in advocateContext.ActEntities.Where(act => repealedInfo.Contains(act.Id))
                       join acttype in advocateContext.ActTyes on act.ActTypeId equals acttype.Id
                       where acttype.IsActive == true
                       select new ActRepealedInfoDto
                       {
                           ActName = act.ActName,
                           Year = act.ActYear,
                           ActNumber = act.ActNumber,
                           ActType = acttype.ActType
                       };
            return data.ToList();

        }

        public int Update_RepealedActByID(int actId, List<int> selectedRepealedAct, string userid)
        {
            var repealedAct = advocateContext.
                RepealedActEntities.
                Where(act => act.ActID == actId).ToList();
            advocateContext.RepealedActEntities.RemoveRange(repealedAct);
            List<RepealedActEntity> repealdList = new List<RepealedActEntity>();
            foreach (var item in selectedRepealedAct)
                repealdList.Add(new RepealedActEntity()
                {
                    UserID = userid,
                    ActID = actId,
                    RepealedActID = item,
                    IsActive = true
                });
            return SaveRepealedAct(repealdList);
        }

        public int Update_AmendedActByID(int actId, List<int> selectedAmendedAct, string userid)
        {
            var amndact = advocateContext.
                   AmendedActEntities.
                   Where(act => act.ActID == actId).ToList();
            advocateContext.AmendedActEntities.RemoveRange(amndact);
            List<AmendedActEntity> amendedActsList = new List<AmendedActEntity>();
            foreach (var item in selectedAmendedAct)
                amendedActsList.Add(new AmendedActEntity()
                {
                    UserID = userid,
                    ActID = actId,
                    AmendedActID = item,
                    IsActive = true
                });
            return SaveAmendedAct(amendedActsList);
        }

        public int Update_ActBookByID(int actId, List<ActBookEntity> actBook, string userid)
        {
            var actbook = advocateContext.
                    ActBookEntities.
                    Where(act => act.ActId == actId).ToList();
            advocateContext.ActBookEntities.RemoveRange(actbook);
            return SaveActBook(actBook);
        }

        public ActDetailDescriptionDto GetActDetailInfoByActId(string userid, int Id)
        {
            var data = (
                        from actdata in advocateContext.ActEntities.Where(act => act.Id == Id && act.IsActive == true && act.UserID == userid)
                        join actType in advocateContext.ActTyes.Where(p => p.IsActive == true) on actdata.ActTypeId equals actType.Id into actType
                        from type in actType.DefaultIfEmpty()

                        join gazetType in advocateContext.gazetteTypeEntities.Where(p => p.IsActive == true) on actdata.GazetteId equals gazetType.Id into gazetDetail
                        from gzedetail in gazetDetail.DefaultIfEmpty()

                        join part in advocateContext.PartEntities.Where(p => p.IsActive == true) on actdata.PartId equals part.Id into partdetail
                        from partd in partdetail.DefaultIfEmpty()

                        join book in advocateContext.ActBookEntities.Where(w => w.IsActive == true) on actdata.Id equals book.ActId into ActBook
                        from acb in ActBook.DefaultIfEmpty()

                        join mstbook in advocateContext.BookMaster.Where(isactive => isactive.IsActive == true) on acb.BookId equals mstbook.Id into BookMaster
                        from bmdetail in BookMaster.DefaultIfEmpty()

                        select new ActDetailDescriptionDto
                        {
                            ActType = type.ActType,
                            ActNumber = actdata.ActNumber,
                            ActYear = actdata.ActYear,
                            AssentBy = actdata.AssentBy,
                            AssentDate = actdata.AssentDate.ToString(),
                            ActName = actdata.ActName,
                            PublishedIn = gzedetail.GazetteName,
                            PartName = partd.PartName,
                            Nature = actdata.Nature,
                            GazetteDate = actdata.GazetteDate.ToString(),
                            PageNo = actdata.PageNo.ToString(),
                            ComeInforce = actdata.ComeInforce,
                            Subjects = actdata.SubjectAct,
                            ActBookList = new List<ActBookDto>()
                            {
                                    new ActBookDto()
                                    {
                                        BookName=bmdetail.BookName,
                                        ShortName=bmdetail.ShortName,
                                        SerialNo=(int)acb.BookSrNo,
                                        Year=acb.BookYear,
                                        Volume=acb.Volume,
                                        PageNo=acb.BookPageNo
                                    }
                            }

                        }
                        );
            var actualData = data;
            ActDetailDescriptionDto actEntity = new ActDetailDescriptionDto();
            actEntity = data.FirstOrDefault();
            if (actEntity.Subjects != null && actEntity.Subjects != "")
            {
                List<int> actSubListIds = actEntity.Subjects.TrimEnd(',').Split(',')
                                            .Select(int.Parse).ToList();
                var subjectData = from sub in advocateContext.Subjects
                                    .Where(sub => actSubListIds.Contains(sub.Id))
                                  select new SubjectEntity
                                  {
                                      Name = sub.Name
                                  };
                actEntity.SubjectList = subjectData.ToList();
            }

            var amndedData = (from amdby in advocateContext.AmendedActEntities
                              .Where(sel => sel.AmendedActID == Id)
                              select amdby.ActID).ToList();

            if (amndedData.ToList().Count > 0)
            {
                var amendeddata = (from act in advocateContext.ActEntities
                                   where amndedData.Contains(act.Id)
                                   join acttype in advocateContext.ActTyes.Where(t => t.IsActive == true)
                                   on act.ActTypeId equals acttype.Id into act_type
                                   from at in act_type.DefaultIfEmpty()
                                   select new ActDto
                                   {
                                       ActCategory = act.ActCategory,
                                       ActName = act.ActName,
                                       ActNumber = act.ActNumber,
                                       ActYear = act.ActYear,
                                       ActType = at.ActType
                                   }).OrderBy(o => o.ActYear);


                actEntity.ActCategory_amended = "Amended";
                actEntity.Amended_ActList = amendeddata.ToList();
            }
            var rpData = (from amdby in advocateContext.RepealedActEntities
                             .Where(sel => sel.RepealedActID == Id)
                          select amdby.ActID).ToList();
            if (rpData.Count > 0)
            {
                var repealedData = (from act in advocateContext.ActEntities
                                    where rpData.Contains(act.Id)
                                    join acttype in advocateContext.ActTyes.Where(t => t.IsActive == true)
                                    on act.ActTypeId equals acttype.Id into act_type
                                    from at in act_type.DefaultIfEmpty()
                                    select new ActDto
                                    {
                                        ActCategory = act.ActCategory,
                                        ActName = act.ActName,
                                        ActNumber = act.ActNumber,
                                        ActYear = act.ActYear,
                                        ActType = at.ActType
                                    }).OrderBy(o => o.ActYear); ;
                actEntity.ActCategory_repealed = "Repealed";
                actEntity.Repealed_ActList = repealedData.ToList();
            }


            var amndedbyData = (from amdby in advocateContext.AmendedActEntities
                               .Where(sel => sel.ActID == Id)
                                select amdby.AmendedActID).ToList();
            if (amndedbyData.Count > 0)
            {
                var amendeddata = (from act in advocateContext.ActEntities
                                   where amndedbyData.Contains(act.Id)
                                   join acttype in advocateContext.ActTyes.Where(t => t.IsActive == true) on act.ActTypeId equals acttype.Id into act_type
                                   from at in act_type.DefaultIfEmpty()
                                   select new ActDto
                                   {
                                       ActCategory = act.ActCategory,
                                       ActName = act.ActName,
                                       ActNumber = act.ActNumber,
                                       ActYear = act.ActYear,
                                       ActType = at.ActType
                                   }).OrderBy(o => o.ActYear);
                actEntity.ActCategory_repealed = "AmendedBy";
                actEntity.AmendedBy_ActList = amendeddata.ToList();

            }

            var rplbyData = (from rpd in advocateContext.RepealedActEntities.Where(sel => sel.ActID == Id) select rpd.RepealedActID).ToList();
            if (rplbyData.Count > 0)
            {
                var repealedData = from act in advocateContext.ActEntities
                                   where rplbyData.Contains(act.Id)
                                   join acttype in advocateContext.ActTyes.Where(t => t.IsActive == true)
                                   on act.ActTypeId equals acttype.Id into act_type
                                   from at in act_type.DefaultIfEmpty()
                                   select new ActDto
                                   {
                                       ActCategory = act.ActCategory,
                                       ActName = act.ActName,
                                       ActNumber = act.ActNumber,
                                       ActYear = act.ActYear,
                                       ActType = at.ActType
                                   };
                actEntity.ActCategory_repealed = "RepealedBy";
                actEntity.RepealedBy_ActList = repealedData.ToList();
            }
            List<ActBookDto> bookActEntity = new List<ActBookDto>();
            foreach (var item in data)
            {
                ActBookDto sactbook = new ActBookDto();
                sactbook.BookName = item.ActBookList.Select(it => it.BookName).FirstOrDefault();
                sactbook.ShortName = item.ActBookList.Select(it => it.ShortName).FirstOrDefault();
                sactbook.SerialNo = item.ActBookList.Select(it => it.SerialNo).FirstOrDefault();
                sactbook.PageNo = item.ActBookList.Select(it => it.PageNo).FirstOrDefault();
                sactbook.Volume = item.ActBookList.Select(it => it.Volume).FirstOrDefault();
                sactbook.Year = item.ActBookList.Select(it => it.Year).FirstOrDefault();
                bookActEntity.Add(sactbook);
            }
            actEntity.ActBookList = bookActEntity.GroupBy(a => a.BookName).Select(g => g.FirstOrDefault()).ToList();
            return actEntity;
        }
    }
}
