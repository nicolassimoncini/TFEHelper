using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchArticleAbstractDTO
    {
        [XmlAttribute("Label")]
        public string Label { get; set; }

        [XmlAttribute("NlmCategory")]
        public string Category { get; set; }

        [XmlText]
        public string Value { get; set; }

        [XmlElement("CopyrightInformation")]
        public string CopyrightInformation { get; set; }
    }
}
