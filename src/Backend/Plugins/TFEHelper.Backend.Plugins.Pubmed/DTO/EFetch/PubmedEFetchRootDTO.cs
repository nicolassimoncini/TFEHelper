using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    [XmlRoot("PubmedArticleSet")]
    public class PubmedEFetchRootDTO
    {
        [XmlElement("PubmedArticle")]
        public List<PubmedEFetchArticleRootDTO> Articles { get; set; } = new List<PubmedEFetchArticleRootDTO>();
    }
}
