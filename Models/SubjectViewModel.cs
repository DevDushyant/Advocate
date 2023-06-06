using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Models
{
    public class SubjectViewModel:BaseViewModel
    {
        [Required(ErrorMessage ="Please enter subject name")]
        [Display(Name ="Subject Name")]
        [StringLength(100,ErrorMessage ="Subject name is not allow more than 100 charactors!")]
        public string Name { get; set; }
    }
}
