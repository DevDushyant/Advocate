using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Models
{
    public class NavigationViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Please enter menu name")]
        [Display(Name = "Menu Name")]
        public string MenuName { get; set; } 
     
        public string Area { get; set; }

        [Required(ErrorMessage = "Please enter Controller")]
        [Display(Name = "Controller")]
        public string Controller { get; set; }

        [Required(ErrorMessage = "Please enter Action Name")]
        [Display(Name = "Action Name")]
        public string ActionName { get; set; }

        [Display(Name = "Is External Url?")]
        public bool IsExternal { get; set; }

        [Display(Name = "External Url?")]
        public string ExternalUrl { get; set; }

        [Required(ErrorMessage = "Please enter display order")]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}
