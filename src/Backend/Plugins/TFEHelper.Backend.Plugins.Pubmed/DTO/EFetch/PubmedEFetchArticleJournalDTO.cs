using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TFEHelper.Backend.Plugins.Pubmed.DTO.Common;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchArticleJournalDTO
    {
        [XmlElement("ISSN")]
        public PubmedISSNDTO ISSN { get; set; }

        [XmlElement("JournalIssue")]
        public PubmedEFetchArticleJournalIssueDTO Issue { get; set; }

        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("ISOAbbreviation")]
        public string ISOAbbreviation { get; set; }
    }
}
