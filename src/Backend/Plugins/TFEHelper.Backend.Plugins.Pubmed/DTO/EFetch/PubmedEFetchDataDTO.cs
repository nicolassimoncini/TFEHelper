using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TFEHelper.Backend.Plugins.Pubmed.DTO.Common;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchDataDTO
    {
        [XmlArray("History")]
        [XmlArrayItem("PubMedPubDate")]
        public List<PubmedDateDTO> History { get; set; }

        [XmlElement("PublicationStatus")]
        public string Status { get; set; }

        [XmlArray("ArticleIdList")]
        [XmlArrayItem("ArticleId")]
        public List<PubmedEFetchArticleIdDTO> ArticleIds { get; set; }
    }
}
