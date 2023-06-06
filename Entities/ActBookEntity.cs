using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities
{
    [Table("Mst_ActBook")]
    public class ActBookEntity:BaseEntity
    {
        [ForeignKey("Act")]
        public int ActId { get; set; }
        public int BookId { get; set; }
        public int BookYear { get; set; }
        public string BookPageNo { get; set; }
        public int? BookSrNo { get; set; }
        public string Volume { get; set; }
        public ActEntity Act { get; set; }

        public static implicit operator List<object>(ActBookEntity v)
        {
            throw new NotImplementedException();
        }
    }
}
