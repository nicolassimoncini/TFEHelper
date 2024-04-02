using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Services.Contracts.DTO.API
{
    public enum SearchSourceDTOType
    {
        [Display(Name = "ACM Digital Library")]
        ACMDigitalLibrary = 0,
        [Display(Name = "SEDICI")]
        SEDICI = 1,
        [Display(Name = "Science Direct")]
        ScienceDirect = 2,
        [Display(Name = "Springer Link")]
        SpringerLink = 3,
        [Display(Name = "IEEE Xplore")]
        IEEEXplore = 4,
        [Display(Name = "Manual")]
        Manual = 5,
        [Display(Name = "Pub Med")]
        PubMed = 6,
        [Display(Name = "Scopus")]
        Scopus = 7,
        [Display(Name = "Eric")]
        Eric = 8,
        [Display(Name = "Doaj")]
        Doaj = 9
    }
}
