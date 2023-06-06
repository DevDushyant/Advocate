using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_BookEntryDetail")]
    public class BookEntryDetailEntity : BaseEntity
    {
        [Column(TypeName = "int")]
        public int BookId { get; set; }

        [Column(TypeName = "int")]
        public int BookYear { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string BookVolume { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string BookPageNo { get; set; }

        [Column(TypeName = "int")]
        public int BookSerialNo { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string DateType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GazetteDate { get; set; }

        [Column(TypeName = "int")]
        public int TypeId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string LegislativeNature { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string TallyType { get; set; }
    }
}
