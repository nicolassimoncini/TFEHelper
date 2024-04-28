using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchArticleAuthorDTO
    {
        [XmlAttribute("ValidYN")]
        public string Valid { get; set; }

        [XmlElement("LastName")]
        public string LastName { get; set; }

        [XmlElement("ForeName")]
        public string ForeName { get; set; }

        [XmlElement("Initials")]
        public string Initials { get; set; }

        [XmlElement("Identifier")]
        public PubmedEFetchArticleAuthorIdentifierDTO Identifier { get; set; }

        [XmlElement("AffiliationInfo")]
        public List<PubmedEFetchArticleAuthorAffiliationInfoDTO> Affiliations { get; set; }
    }
}
