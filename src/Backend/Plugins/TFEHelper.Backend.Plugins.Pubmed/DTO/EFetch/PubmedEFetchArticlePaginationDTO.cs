using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchArticlePaginationDTO
    {
        [XmlElement("StartPage")]
        public string StartPage { get; set; }

        [XmlElement("EndPage")]
        public string EndPage { get; set; }

        [XmlElement("MedlinePgn")]
        public string MedlinePageNumber { get; set; }
    }
}
