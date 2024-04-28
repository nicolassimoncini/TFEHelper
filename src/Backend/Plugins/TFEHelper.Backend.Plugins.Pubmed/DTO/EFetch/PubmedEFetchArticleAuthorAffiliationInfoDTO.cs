using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchArticleAuthorAffiliationInfoDTO
    {
        [XmlElement("Affiliation")]
        public string Affiliation { get; set; }
    }
}
