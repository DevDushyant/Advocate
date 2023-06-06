using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_NavigationMenus")]
    public class NavigationEntity : BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string MenuName { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string MenuCode { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Area { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Controller { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ActionName { get; set; }
        [Column(TypeName = "bool")]
        public bool IsExternal { get; set; }
        public string ExternalUrl { get; set; }
        [Column(TypeName = "int")]
        public int DisplayOrder { get; set; }
    }
}
