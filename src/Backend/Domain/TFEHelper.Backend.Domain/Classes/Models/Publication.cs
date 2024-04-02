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
        public string Key { get; set; } = string.Empty;
        [BibTeXKey]
        [Required]
        public SearchSourceType Source { get; set; }
        public string URL { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = string.Empty; 
        public string Authors { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        public string DOI { get; set; } = string.Empty;
        public int Year { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string ISSN { get; set; } = string.Empty;
        public string Abstract { get; set; } = string.Empty;
        public string Pages { get; set; } = string.Empty;
    }
}
