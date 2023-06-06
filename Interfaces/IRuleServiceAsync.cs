using Advocate.Dtos;
using Advocate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Interfaces
{
   public interface IRuleServiceAsync : IGenericServiceAsync<RuleEntity>
    {
        IEnumerable<RuleDto> GetRule();
        IEnumerable<RuleDto> GetRuleByActId(int actId,string ruleKind);
        RuleDetailInfoDto GetRuleDetailByRuleId(int ruleId);
        RuleDetailReportDto GetRuleDetailReportbyRuleId(int ruleId);
        int LastInsertedRecord();
        int SaveOrUpdateRuleExtraAct(List<RuleActExtraEntity> extraEntity, int ruleId);
        int SaveOrUpdateRuleBook(List<RuleBookEntity> ruleBookEntity, int ruleId);
        int SaveorUpdateRuleAmended(List<RuleAmendedEntity> ruleAmendedEntity, int ruleId);
        int SaveOrUpdateRuleRepealed(List<RuleRepealedEntity> ruleRepealeds, int ruleId);
        int DeleteRuleBook(RuleBookEntity ruleBookEntity);
        int DeleteAmendedRule(RuleAmendedEntity ruleAmendedEntity);
        int DeleteRepealedRule(RuleRepealedEntity repealedEntity);
        int DeleteExtraAct(RuleActExtraEntity extraEntity);
    }
}
