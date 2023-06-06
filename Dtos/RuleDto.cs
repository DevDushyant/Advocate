using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Dtos
{
    public class RuleDto:BaseDto
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

    }
}
