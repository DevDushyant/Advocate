using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name ="First name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        //[Display(Name = "DOB")]
        public DateTime DOB { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<string> AssinedRole { get; set; }

        //[Display(Name = "Contact")]
        //public string Contact { get; set; }

        public SelectList Roles { get; set; }
        public IdentityUser User { get; set; }
    }
}
