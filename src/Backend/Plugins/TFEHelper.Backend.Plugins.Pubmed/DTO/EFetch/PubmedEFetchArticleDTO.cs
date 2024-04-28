using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TFEHelper.Backend.Plugins.Pubmed.DTO.Common;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchArticleDTO
    {
        [XmlAttribute("PubModel")]
        public string PublicationModel { get; set; }

        [XmlElement("Journal")]
        public PubmedEFetchArticleJournalDTO Journal { get; set; }

        [XmlElement("ArticleTitle")]
        public string Title { get; set; }

        [XmlElement("Pagination")]
        public PubmedEFetchArticlePaginationDTO Pagination { get; set; }

        [XmlElement("ELocationID")]
        public PubmedEFetchArticleELocationIdDTO Location { get; set; }

        [XmlArray("Abstract")]
        [XmlArrayItem("AbstractText")]
        public List<PubmedEFetchArticleAbstractDTO> Abstracts { get; set; }

        [XmlElement("AuthorList")]
        public PubmedEFetchArticleAuthorListDTO Authors { get; set; }

        [XmlElement("Language")]
        public string Language { get; set; }

        [XmlElement("PublicationTypeList")]
        public PubmedEFetchArticlePublicationTypeListDTO PublicationTypes { get; set; }

        [XmlElement("ArticleDate")]
        public PubmedDateDTO Date { get; set; }
    }
}
