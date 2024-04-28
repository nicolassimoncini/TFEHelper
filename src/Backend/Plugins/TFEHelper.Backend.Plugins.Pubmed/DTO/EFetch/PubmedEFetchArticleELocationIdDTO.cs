using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchArticleELocationIdDTO
    {
        [XmlAttribute("EIdType")]
        public string Type { get; set; }

        [XmlAttribute("ValidYN")]
        public string Valid { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
