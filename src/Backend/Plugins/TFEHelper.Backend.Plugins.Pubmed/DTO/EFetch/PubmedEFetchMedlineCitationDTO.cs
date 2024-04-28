using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TFEHelper.Backend.Plugins.Pubmed.DTO.Common;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchMedlineCitationDTO
    {
        [XmlAttribute("Status")]
        public string Status { get; set; }

        [XmlAttribute("Owner")]
        public string Owner { get; set; }

        [XmlAttribute("IndexingMethod")]
        public string IndexingMethod { get; set; }

        [XmlElement("PMID")]
        public PubmedPMIDDTO PMID { get; set; }

        [XmlElement("DateRevised")]
        public PubmedDateDTO DateRevised { get; set; }

        [XmlElement("Article")]
        public PubmedEFetchArticleDTO Article { get; set; }

        [XmlElement("MedlineJournalInfo")]
        public PubmedEFetchMedlineJournalInfoDTO JournalInfoDTO { get; set; }

        [XmlElement("CitationSubset")]
        public string CitationSubset { get; set; }

        [XmlElement("KeywordList")]
        public PubmedEFetchKeywordListDTO Keywords { get; set; }
    }
}
