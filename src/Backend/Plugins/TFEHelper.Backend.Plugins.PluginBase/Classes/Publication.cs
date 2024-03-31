using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Enums;

namespace TFEHelper.Backend.Plugins.PluginBase.Classes
{
    public class Publication
    {
        public BibTeXPublicationType Type { get; set; }
        public string Key { get; set; }
        public SearchSourceType Source { get; set; }
        public string URL { get; set; }
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
