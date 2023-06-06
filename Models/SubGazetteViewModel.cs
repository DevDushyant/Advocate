using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Models
{
    public class SubGazetteViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Please enter part name!")]
        [Display(Name = "Part Name")]
        public string PartName { get; set; }

        //[Required(ErrorMessage = "Please select gazette name!")]
        [Display(Name = "Gazette Name")]
        public string gazetteName { get; set; }
        public int GazzetId { get; set; }
        public SelectList GazzetList { get; set; }
    }
}
