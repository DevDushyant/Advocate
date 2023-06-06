using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Dtos
{
    public class RuleDetailReportDto
    {

        public string GazzetName { get; set; }
        public string ActType { get; set; }
        public string ActName { get; set; }
        public string RuleName { get; set; }
        public string RuleNo { get; set; }
        public string RuleType { get; set; }
        public string Nature { get; set; }
        public int ActTypeId { get; set; }
        public int ActId { get; set; }
        public string RuleDate { get; set; }
        public string GSRSO_Prefix { get; set; }
        public string GSRSO_No { get; set; }
        public int? GazzetId { get; set; }
        public int? PartId { get; set; }
        public DateTime? GazzetDate { get; set; }
        public int PageNo { get; set; }
        public string ComeInforce { get; set; }
        public DateTime? ComeInforceEFDate { get; set; }
        public bool isExtra { get; set; }
        public string AmendedRules { get; set; }
        public string RepealedRules { get; set; }
        public List<RuleBookDto> RuleBookList { get; set; }
        public List<RuleDto> RepealedRuleList { get; set; }
        public List<RuleDto> AmendedRuleList { get; set; }
        public List<RuleDto> RepealedRuleBy { get; set; }
        public List<RuleDto> AmendedRuleBy { get; set; }
    }
    public class RuleBookDto
    {
        public string BookaName { get; set; }
        public int Year { get; set; }
        public string PageNo { get; set; }
        public int serailnumber { get; set; }
        public string Volume { get; set; }
    }
}
