using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Models
{
    public class BookEntryDetailViewModel:BaseViewModel
    {
        public int BookId { get; set; }
        public int BookYear { get; set; }
        public string BookVolume { get; set; }
        public string BookPageNo { get; set; }
        public int BookSerialNo { get; set; }
        public string DateType { get; set; }
        public string GazetteDate { get; set; }
        public int TypeId { get; set; }
        public string LegislativeNature { get; set; }        
        public string TallyType { get; set; }
    }
}
