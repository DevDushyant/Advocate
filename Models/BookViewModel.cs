using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Models
{
    public class BookViewModel: BaseViewModel
    {
        [Required(ErrorMessage = "Please enter book name")]
        [MaxLength(100)]
        [Display(Name = "Book Name")]
        public string BookName { get; set; }

       
        [MaxLength(30)]
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }
        //public SelectList BookTypeList { get; set; }
        //public SelectList VolumeList { get; set; }

        //[Required(ErrorMessage = "Please enter book name")]
        //[Display(Name = "Book Name")]
        //public string BookName { get; set; }
        //[Required(ErrorMessage = "Please enter publisher name")]
        //[Display(Name = "Publisher Name")]
        //public string PublisherName { get; set; }
        //[Required(ErrorMessage = "Please enter book auther name")]
        //[Display(Name = "Auther")]
        //public string Auther { get; set; }

        //[Required(ErrorMessage = "Please select volume")]
        //[Display(Name = "Volume")]
        //public virtual string Volume { get; set; }

        //[Required(ErrorMessage = "Please select book type")]
        //[Display(Name = "Book Type")]
        //public string BookType { get; set; }

        //[Required(ErrorMessage = "Please enter publish year")]
        //[Display(Name = "Publishing Year")]
        //public DateTime PublishYear { get; set; }
    }
}
