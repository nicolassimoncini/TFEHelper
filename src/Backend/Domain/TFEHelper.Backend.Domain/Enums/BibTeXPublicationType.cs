using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Enums
{
    public enum BibTeXPublicationType
    {
        [Display(Name = "Article")]
        Article,
        [Display(Name = "Book")]
        Book,
        [Display(Name = "Booklet")]
        Booklet,
        [Display(Name = "Conference")]
        Conference,
        [Display(Name = "In collection")]
        InCollection,
        [Display(Name = "In proceedings")]
        InProceedings,
        [Display(Name = "In book")]
        InBook,
        [Display(Name = "Manual")]
        Manual,
        [Display(Name = "Master thesis")]
        MasterThesis,
        [Display(Name = "Miscelaneous")]
        Misc,
        [Display(Name = "PHD thesis")]
        PHDThesis,
        [Display(Name = "Proceedings")]
        Proceedings,
        [Display(Name = "Tech report")]
        Techreport,
        [Display(Name = "Unpublished")]
        Unpublished
    }
}
