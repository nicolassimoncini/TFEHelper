using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchArticleRootDTO
    {
        [XmlElement("MedlineCitation")]
        public PubmedEFetchMedlineCitationDTO Citation { get; set; }

        [XmlElement("PubmedData")]
        public PubmedEFetchDataDTO Data { get; set; }
    }
}
