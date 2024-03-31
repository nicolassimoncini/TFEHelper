using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.SpringerLink.Enums
{
    // https://dev.springernature.com/adding-constraints
    public enum SpringerLinkSearchInElementType
    {
        //Abstract, // Springer Link no filtra por "Abstract" en la API
        [Display(Name = "all")]
        All,
        [Display(Name = "keyword")]
        Keywords,
        [Display(Name = "title")]
        Title
    }
}
