using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.Common
{
    public class PubmedDateDTO
    {
        [XmlAttribute("PubStatus")]
        public string Status { get; set; }

        [XmlAttribute("DateType")]
        public string Type { get; set; }

        [XmlElement("Year")]
        public int Year { get; set; }

        [XmlElement("Month")]
        public string Month { get; set; }

        [XmlElement("Day")]
        public int Day { get; set; }

        [XmlElement("Hour")]
        public int Hour { get; set; }

        [XmlElement("Minute")]
        public int Minute { get; set; }
    }
}
