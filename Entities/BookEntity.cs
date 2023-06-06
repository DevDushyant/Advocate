using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advocate.Entities
{
    [Table("Mst_Books")]
    public class BookEntity:BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string BookName { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string ShortName { get; set; }
    }
}
