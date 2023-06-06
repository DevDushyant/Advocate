using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Models
{
    public class GazetteViewModel:BaseViewModel
    {
        [Required(ErrorMessage ="Please enter gazette name!")]
        [Display(Name ="Gazette Name")]
        public string GazetteName { get; set; }
    }
}
