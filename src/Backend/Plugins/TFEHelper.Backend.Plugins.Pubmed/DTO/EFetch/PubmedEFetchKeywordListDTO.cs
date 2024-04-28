using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchKeywordListDTO
    {
        [XmlAttribute("Owner")]
        public string Owner { get; set; }

        [XmlElement("Keyword")]
        public List<PubmedEFetchKeywordDTO> Items { get; set; }
    }
}
