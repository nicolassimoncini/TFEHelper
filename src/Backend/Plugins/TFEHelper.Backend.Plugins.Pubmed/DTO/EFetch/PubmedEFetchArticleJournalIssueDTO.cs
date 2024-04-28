using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TFEHelper.Backend.Plugins.Pubmed.DTO.Common;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchArticleJournalIssueDTO
    {
        [XmlAttribute("CitedMedium")]
        public string CitedMedium { get; set; }

        [XmlElement("PubDate")]
        public PubmedDateDTO Date { get; set; }
    }
}
