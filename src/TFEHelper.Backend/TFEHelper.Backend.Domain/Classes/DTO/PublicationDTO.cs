using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TFEHelper.Backend.Domain.Attributes;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Domain.Interfaces;

namespace TFEHelper.Backend.Domain.Classes.DTO
{
    public class PublicationDTO : ITFEHelperDTO
    {
        public int Id { get; set; }
        [Required]
        public BibTeXPublicationType Type { get; set; }
        public string Key { get; set; }
        [Required]
        public SearchSourceType Source { get; set; }
        public string URL { get; set; }
        [Required]
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Keywords { get; set; }
        public string DOI { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        public string ISSN { get; set; }
        public string Abstract { get; set; }
        public string Pages { get; set; }
    }
}
