using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Attributes;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Domain.Interfaces;

namespace TFEHelper.Backend.Domain.Classes.Models
{
    public class Publication : IBibTeXRecord, ITFEHelperModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [BibTeXKey]
        [Required]
        public BibTeXPublicationType Type { get; set; }
        [BibTeXKey]
        public string Key { get; set; }
        [BibTeXKey]
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
        [Required]
        public string Abstract { get; set; }
        public string Pages { get; set; }
    }
}
