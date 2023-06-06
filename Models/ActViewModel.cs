using Advocate.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Models
{
    public class ActViewModel:BaseViewModel
    {
        public string ActCategory { get; set; }
        public int ActTypeId { get; set; }
        public int ActNumber { get; set; }
        public int ActYear { get; set; }
        public string AssentBy { get; set; }
        public string AssentDate { get; set; }
        public string ActName { get; set; }
        public int PublishedInId { get; set; }
        public string PublishedGazetteName { get; set; }
        public int PartId { get; set; }
        public string GazetteNuture { get; set; }
        public string GazetteDate { get; set; }
        public int PageNumber { get; set; }
        public string ComeInforce { get; set; }
        public string SubjectAct { get; set; }
        public List<int> selectedActListId { get; set; }
        public List<ActBook> actBookList { get; set; }
    }
}
