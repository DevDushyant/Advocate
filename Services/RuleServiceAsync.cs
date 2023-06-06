using Advocate.Data;
using Advocate.Dtos;
using Advocate.Entities;
using Advocate.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Services
{
    public class RuleServiceAsync : GenericServiceAsync<RuleEntity>, IRuleServiceAsync
    {
        private readonly AdvocateContext advocateContext;
        private readonly IMapper _mapper;
        public RuleServiceAsync(AdvocateContext context, IMapper _mapper) : base(context)
        {
            advocateContext = context;
            this._mapper = _mapper;
        }

        public int DeleteAmendedRule(RuleAmendedEntity ruleAmendedEntity)
        {
            advocateContext.Remove(ruleAmendedEntity);
            return advocateContext.SaveChanges();
        }

        public int DeleteExtraAct(RuleActExtraEntity extraEntity)
        {
            advocateContext.Remove(extraEntity);
            return advocateContext.SaveChanges();
        }

        public int DeleteRepealedRule(RuleRepealedEntity repealedEntity)
        {
            advocateContext.Remove(repealedEntity);
            return advocateContext.SaveChanges();
        }

        public int DeleteRuleBook(RuleBookEntity ruleBookEntity)
        {
            advocateContext.Remove(ruleBookEntity);
            return advocateContext.SaveChanges();

        }
        public IEnumerable<RuleDto> GetRule()
        {
            var result = from rule in advocateContext.RuleEntities.Where(active => active.IsActive == true)
                         join gzt in advocateContext.gazetteTypeEntities.Where(active => active.IsActive == true) on rule.GazzetId equals gzt.Id into guzzetData
                         from gdata in guzzetData.DefaultIfEmpty()
                         join act in advocateContext.ActEntities.Where(active => active.IsActive == true) on rule.ActId equals act.Id into actData
                         from adata in actData.DefaultIfEmpty()
                         join acttype in advocateContext.ActTyes.Where(active => active.IsActive == true) on rule.ActTypeId equals acttype.Id into acttypedata
                         from typedata in acttypedata.DefaultIfEmpty()
                         select new RuleDto
                         {
                             RuleName = rule.RuleName,
                             RuleNo = rule.RuleNo,
                             RuleType = rule.RuleType == "AMD" ? "Amended" : rule.RuleType == "RPD" ? "Repealed" : rule.RuleType,
                             ActName = adata.ActName,
                             ActType = typedata.ActType,
                             GazzetName = gdata.GazetteName,
                             Id = rule.Id,
                             Nature = rule.Nature,
                             ActTypeId = rule.ActTypeId,
                             ActId = rule.ActId,
                             RuleDate = rule.RuleDate.Value.ToString("dd/MM/yyyy")
                         };
            return result.ToList();
        }

        public IEnumerable<RuleDto> GetRuleByActId(int actId, string ruleKind)
        {
            IEnumerable<RuleDto> result;
            if (ruleKind == "ALL")
            {
                result = from rule in advocateContext.RuleEntities.Where(active => active.IsActive == true)
                         select new RuleDto { Id = rule.Id, RuleName = rule.RuleName };
            }
            else
            {
                result = from rule in advocateContext.RuleEntities.Where(active => active.IsActive == true && active.ActId == actId && active.RuleType == ruleKind)
                         select new RuleDto { Id = rule.Id, RuleName = rule.RuleName };
            }
            return result.ToList().OrderBy(o => o.RuleName);
        }

        public RuleDetailInfoDto GetRuleDetailByRuleId(int ruleId)
        {

            var result = from rule in advocateContext.RuleEntities.Where(active => active.IsActive == true && active.Id == ruleId)
                         select new RuleDetailInfoDto
                         {
                             RuleName = rule.RuleName,
                             RuleNo = rule.RuleNo,
                             RuleType = rule.RuleType,
                             Id = rule.Id,
                             Nature = rule.Nature,
                             ActTypeId = rule.ActTypeId,
                             ActId = rule.ActId,
                             ComeInforce = rule.ComeInforce,
                             ComeInforceEFDate = rule.ComeInforceEFDate,
                             GazzetDate = rule.GazzetDate,
                             GazzetId = rule.GazzetId,
                             GSRSO_No = rule.GSRSO_No,
                             GSRSO_Prefix = rule.GSRSO_Prefix,
                             PageNo = rule.PageNo,
                             PartId = rule.PartId,
                             RuleDate = rule.RuleDate.Value.ToString("dd/MM/yyyy")
                         };
            var ruleAmended = from ruleamnd in advocateContext.RuleAmendedEntities.Where(active => active.IsActive == true && active.AmendedRuleID == ruleId)
                              select new AmendedRule
                              {
                                  RuleId = ruleamnd.RuleId
                              };
            var ruleRepealed = from rrpld in advocateContext.RuleRepealedEntities.Where(active => active.IsActive == true && active.RepealedRuleID == ruleId)
                               select new RepealedRule
                               {
                                   RuleId = rrpld.RuleId
                               };
            var ruleBook = from rbook in advocateContext.RuleBookEntities.Where(active => active.IsActive == true && active.RuleId == ruleId)
                           select new RuleBook
                           {
                               RuleId = rbook.RuleId,
                               BookId = rbook.BookId,
                               BookPageNo = rbook.BookPageNo,
                               BookSrNo = rbook.BookSrNo,
                               Volume = rbook.Volume,
                               BookYear = rbook.BookYear
                           };
            var extraActRule = from extRuleAct in advocateContext.RuleActExtraEntities.Where(record => record.IsActive == true && record.RuleId == ruleId)
                               select new ExtraAct
                               {
                                   ActId = extRuleAct.ActId
                               };

            RuleDetailInfoDto ruleDetailInfoDto = new RuleDetailInfoDto();
            ruleDetailInfoDto.RuleBookList = ruleBook.ToList();
            ruleDetailInfoDto.AmendedRuleList = ruleAmended.ToList();
            ruleDetailInfoDto.RepealedRuleList = ruleRepealed.ToList();
            ruleDetailInfoDto.RuleName = result.Select(r => r.RuleName).FirstOrDefault();
            ruleDetailInfoDto.RuleNo = result.Select(r => r.RuleNo).FirstOrDefault();
            ruleDetailInfoDto.RuleType = result.Select(r => r.RuleType).FirstOrDefault();
            ruleDetailInfoDto.Id = result.Select(r => r.Id).FirstOrDefault();
            ruleDetailInfoDto.Nature = result.Select(r => r.Nature).FirstOrDefault();
            ruleDetailInfoDto.ActTypeId = result.Select(r => r.ActTypeId).FirstOrDefault();
            ruleDetailInfoDto.ActId = result.Select(r => r.ActId).FirstOrDefault();
            ruleDetailInfoDto.ComeInforce = result.Select(r => r.ComeInforce).FirstOrDefault();
            ruleDetailInfoDto.ComeInforceEFDate = result.Select(r => r.ComeInforceEFDate).FirstOrDefault();
            ruleDetailInfoDto.GazzetDate = result.Select(r => r.GazzetDate).FirstOrDefault();
            ruleDetailInfoDto.GazzetId = result.Select(r => r.GazzetId).FirstOrDefault();
            ruleDetailInfoDto.GSRSO_No = result.Select(r => r.GSRSO_No).FirstOrDefault();
            ruleDetailInfoDto.GSRSO_Prefix = result.Select(r => r.GSRSO_Prefix).FirstOrDefault();
            ruleDetailInfoDto.PageNo = result.Select(r => r.PageNo).FirstOrDefault();
            ruleDetailInfoDto.PartId = result.Select(r => r.PartId).FirstOrDefault();
            ruleDetailInfoDto.RuleDate = result.Select(r => r.RuleDate).FirstOrDefault();
            ruleDetailInfoDto.ExtraActList = extraActRule.ToList();
            //ruleDetailInfoDto.RepealedRules = result.Select(r => r.RepealedRules).FirstOrDefault();
            return ruleDetailInfoDto;
        }

        public RuleDetailReportDto GetRuleDetailReportbyRuleId(int ruleId)
        {
            var rulebasicInfo = (from rule in advocateContext.RuleEntities.Where(active => active.IsActive == true && active.Id == ruleId)
                                 join gzt in advocateContext.gazetteTypeEntities.Where(active => active.IsActive == true) on rule.GazzetId equals gzt.Id into guzzetData
                                 from gdata in guzzetData.DefaultIfEmpty()
                                 join act in advocateContext.ActEntities.Where(active => active.IsActive == true) on rule.ActId equals act.Id into actData
                                 from adata in actData.DefaultIfEmpty()
                                 join acttype in advocateContext.ActTyes.Where(active => active.IsActive == true) on rule.ActTypeId equals acttype.Id into acttypedata
                                 from typedata in acttypedata.DefaultIfEmpty()
                                 select new RuleDetailReportDto
                                 {
                                     RuleName = rule.RuleName,
                                     RuleNo = rule.RuleNo,
                                     RuleType = rule.RuleType == "AMD" ? "Amended" : rule.RuleType == "RPD" ? "Repealed" : rule.RuleType,
                                     ActName = adata.ActName,
                                     ActType = typedata.ActType,
                                     GazzetName = gdata.GazetteName,
                                     Nature = rule.Nature,
                                     ActTypeId = rule.ActTypeId,
                                     ActId = rule.ActId,
                                     RuleDate = rule.RuleDate.Value.ToString("dd/MM/yyyy")
                                 }).FirstOrDefault();

            var ruleBook = from rbook in advocateContext.RuleBookEntities.Where(active => active.IsActive == true && active.RuleId == ruleId)
                           join b in advocateContext.BookMaster.Where(b => b.IsActive == true) on rbook.Id equals b.Id into mstb
                           from mstbook in mstb.DefaultIfEmpty()
                           select new RuleBookDto
                           {
                               BookaName = mstbook.BookName,
                               PageNo = rbook.BookPageNo,
                               serailnumber = rbook.BookSrNo.Value,
                               Volume = rbook.Volume,
                               Year = rbook.BookYear
                           };

            IQueryable<RuleDto> result = (from rule in advocateContext.RuleEntities                                         
                                          join gzt in advocateContext.gazetteTypeEntities.Where(active => active.IsActive == true) on rule.GazzetId equals gzt.Id into guzzetData
                                          from gdata in guzzetData.DefaultIfEmpty()
                                          join act in advocateContext.ActEntities.Where(active => active.IsActive == true) on rule.ActId equals act.Id into actData
                                          from adata in actData.DefaultIfEmpty()
                                          join acttype in advocateContext.ActTyes.Where(active => active.IsActive == true) on rule.ActTypeId equals acttype.Id into acttypedata
                                          from typedata in acttypedata.DefaultIfEmpty()
                                          select new RuleDto
                                          {
                                              RuleName = rule.RuleName,
                                              RuleNo = rule.RuleNo,
                                              RuleType = rule.RuleType == "AMD" ? "Amended" : rule.RuleType == "RPD" ? "Repealed" : rule.RuleType,
                                              ActName = adata.ActName,
                                              ActType = typedata.ActType,
                                              GazzetName = gdata.GazetteName,
                                              Id = rule.Id,
                                              Nature = rule.Nature,
                                              ActTypeId = rule.ActTypeId,
                                              ActId = rule.ActId,
                                              RuleDate = rule.RuleDate.Value.ToString("dd/MM/yyyy")
                                          });

            var amndedData = (from amdby in advocateContext.RuleAmendedEntities
                                .Where(sel => sel.AmendedRuleID == ruleId)
                              select amdby.RuleId).ToList();

            var amDataby = (from amdby in advocateContext.RuleAmendedEntities
                               .Where(sel => sel.RuleId == ruleId)
                              select amdby.AmendedRuleID).ToList();

            if (amndedData.ToList().Count > 0)
            {
                var data= from amndataf in result where amndedData.Contains(amndataf.Id) select amndataf;
                //actEntity.ActCategory_repealed = "Amended";
                rulebasicInfo.AmendedRuleList = data.ToList();
            }

            if (amDataby.ToList().Count > 0)
            {
                var data = from dt in result where amDataby.Contains(dt.Id) select dt;
                //actEntity.ActCategory_repealed = "AmendedBy";
                rulebasicInfo.AmendedRuleBy = data.ToList();
            }

            var rplData = (from amdby in advocateContext.RuleRepealedEntities
                               .Where(sel => sel.RepealedRuleID == ruleId)
                              select amdby.RuleId).ToList();
            var rplDataBy = (from amdby in advocateContext.RuleRepealedEntities
                              .Where(sel => sel.RuleId == ruleId)
                           select amdby.RepealedRuleID).ToList();
            if (rplData.ToList().Count > 0)
            {
                var data = from dt in result where rplData.Contains(dt.Id) select dt;
                //actEntity.ActCategory_repealed = "Repealed";
                rulebasicInfo.RepealedRuleList = data.ToList();
            }
            if (rplDataBy.ToList().Count > 0)
            {
                var data = from dt in result where rplDataBy.Contains(dt.Id) select dt;
                //actEntity.ActCategory_repealed = "RepealedBy";
                rulebasicInfo.RepealedRuleBy = data.ToList();
            }

            return rulebasicInfo;
        }

        public int LastInsertedRecord()
        {
            return advocateContext.RuleEntities.Where(row => row.IsActive == true).Max(row => row.Id);
        }

        public int SaveorUpdateRuleAmended(List<RuleAmendedEntity> ruleAmendedEntity, int ruleId)
        {
            if (ruleAmendedEntity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                if (ruleId != 0)
                {
                    var ruleAmnd = advocateContext.RuleAmendedEntities.Where(ra => ra.RuleId == ruleId).ToList();
                    advocateContext.RuleAmendedEntities.RemoveRange(ruleAmnd);
                }
                ruleAmendedEntity.ForEach(item => advocateContext.RuleAmendedEntities.Add(item));
                return advocateContext.SaveChanges();
            }
        }

        public int SaveOrUpdateRuleBook(List<RuleBookEntity> ruleBookEntity, int ruleId)
        {
            if (ruleBookEntity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                if (ruleId != 0)
                {
                    var actbook = advocateContext.RuleBookEntities.Where(rbook => rbook.RuleId == ruleId).ToList();
                    advocateContext.RuleBookEntities.RemoveRange(actbook);
                }
                ruleBookEntity.ForEach(item => advocateContext.RuleBookEntities.Add(item));
                return advocateContext.SaveChanges();
            }
        }

        public int SaveOrUpdateRuleExtraAct(List<RuleActExtraEntity> extraEntity, int ruleId)
        {
            if (extraEntity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                if (ruleId != 0)
                {
                    var extRule = advocateContext.RuleActExtraEntities.Where(extRule => extRule.RuleId == ruleId).ToList();
                    advocateContext.RuleActExtraEntities.RemoveRange(extRule);
                }
                extraEntity.ForEach(item => advocateContext.RuleActExtraEntities.Add(item));
                return advocateContext.SaveChanges();
            }
        }

        public int SaveOrUpdateRuleRepealed(List<RuleRepealedEntity> ruleRepealeds, int ruleId)
        {
            if (ruleRepealeds == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                if (ruleId != 0)
                {
                    var rpRule = advocateContext.RuleRepealedEntities.Where(rprule => rprule.RuleId == ruleId).ToList();
                    advocateContext.RuleRepealedEntities.RemoveRange(rpRule);
                }
                ruleRepealeds.ForEach(item => advocateContext.RuleRepealedEntities.Add(item));
                return advocateContext.SaveChanges();
            }
        }





    }
}
