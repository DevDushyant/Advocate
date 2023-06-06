using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Models
{
    public class NotificationTypesViewModel:BaseViewModel
    {
        [Required(ErrorMessage = "Please enter notification name")]
        [Display(Name = "Notification Type")]
        [StringLength(100, ErrorMessage = "Notification Type is not allow more than 100 charactors!")]
        public string Name { get; set; }
    }
}
