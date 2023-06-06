using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_Rule")]
    public class RuleEntity : BaseEntity
    {
        [Column(TypeName = "int")]
        public int ActTypeId { get; set; }

        [Column(TypeName = "int")]
        public int ActId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string RuleNo { get; set; }

        [Column(TypeName = "varchar(5)")]
        public string GSRSO_Prefix { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string GSRSO_No { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string RuleType { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string RuleName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RuleDate { get; set; }

        [Column(TypeName = "int")]
        public int? GazzetId { get; set; }

        [Column(TypeName = "int")]
        public int? PartId { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Nature { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GazzetDate { get; set; }

        [Column(TypeName = "int")]
        public int PageNo { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string ComeInforce { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ComeInforceEFDate { get; set; }
    }
}
