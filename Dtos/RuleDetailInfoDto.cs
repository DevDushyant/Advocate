using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Dtos
{
    public class RuleDetailInfoDto
    {
        public int Id { get; set; }
        public int ActTypeId { get; set; }
        public int ActId { get; set; }
        public string RuleNo { get; set; }
        public string GSRSO_Prefix { get; set; }
        public string GSRSO_No { get; set; }
        public string RuleType { get; set; }
        public string RuleName { get; set; }
        public string RuleDate { get; set; }
        public int? GazzetId { get; set; }
        public int? PartId { get; set; }
        public string Nature { get; set; }
        public DateTime? GazzetDate { get; set; }
        public int PageNo { get; set; }
        public string ComeInforce { get; set; }
        public DateTime? ComeInforceEFDate { get; set; }
        public bool isExtra { get; set; }
        public string AmendedRules { get; set; }
        public string RepealedRules { get; set; }
        public List<RuleBook> RuleBookList { get; set; }
        public List<RepealedRule> RepealedRuleList { get; set; }
        public List<AmendedRule> AmendedRuleList { get; set; }
        public List<ExtraAct> ExtraActList { get; set; }
    }
    public class RuleBook
    {
        public int RuleId { get; set; }
        public int BookId { get; set; }
        public int BookYear { get; set; }
        public string BookPageNo { get; set; }
        public int? BookSrNo { get; set; }
        public string Volume { get; set; }
    }
    public class RepealedRule
    {
        public int RuleId { get; set; }
        public int RepealedRuleID { get; set; }

    }
    public class AmendedRule
    {
        public int RuleId { get; set; }
        public int AmendedRuleID { get; set; }
    }

    public class ExtraAct
    {
        public int ActId { get; set; }
    }
}
