using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchArticleAuthorListDTO
    {
        [XmlAttribute("CompleteYN")]
        public string Complete { get; set; }

        [XmlElement("Author")]
        public List<PubmedEFetchArticleAuthorDTO> Items { get; set; }
    }
}
