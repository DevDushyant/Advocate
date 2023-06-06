using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Models
{
    public class ActTypeViewModel:BaseViewModel
    {
        [Display(Name = "Act Type")]
        [Required(ErrorMessage ="Please enter act type!")]
        public string ActType { get; set; }
    }
}
