using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.Common
{
    public class PubmedPMIDDTO
    {
        [XmlAttribute("Version")]
        public string Version { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
