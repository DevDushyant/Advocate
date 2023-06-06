using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_Act")]
    public class ActEntity : BaseEntity
    {
        [Column(TypeName = "varchar(10)")]
        public string ActCategory { get; set; }

        [Column(TypeName = "int")]
        public int ActTypeId { get; set; }

        [Column(TypeName = "int")]
        public int ActNumber { get; set; }

        [Column(TypeName = "varchar(3)")]
        public int SubActNumber { get; set; }

        [Column(TypeName = "int")]
        public int ActYear { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string AssentBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AssentDate { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string ActName { get; set; }

        [Column("PublishedIn", TypeName = "int")]
        public int GazetteId { get; set; }

        [Column(TypeName = "int")]
        public int PartId { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Nature { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GazetteDate { get; set; }

        [Column(TypeName = "int")]
        public int? PageNo { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string ComeInforce { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string SubjectAct { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PublishedInGazeteDate { get; set; }
        public virtual List<AmendedActEntity> AmendedActList { get; set; }
        public virtual List<RepealedActEntity> RepealedActList { get; set; }
        public virtual List<ActBookEntity> ActBookList { get; set; }
    }
}
