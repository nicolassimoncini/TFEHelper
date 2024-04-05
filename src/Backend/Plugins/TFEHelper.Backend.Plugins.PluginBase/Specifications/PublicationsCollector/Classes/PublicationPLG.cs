using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Enums;

namespace TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes
{
    public class PublicationPLG
    {
        public required BibTeXPublicationPLGType Type { get; set; }
        public string Key { get; set; }
        public required SearchSourcePLGType Source { get; set; }
        public string URL { get; set; }
        public required string Title { get; set; }
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
