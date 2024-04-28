using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchMedlineJournalInfoDTO
    {
        [XmlElement("Country")]
        public string Country { get; set; }

        [XmlElement("MedlineTA")]
        public string MedlineTA { get; set; }

        [XmlElement("NlmUniqueID")]
        public string NImUniqueID { get; set; }

        [XmlElement("ISSNLinking")]
        public string ISSNLinking { get; set; }
    }
}
