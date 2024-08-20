using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Scopus.DTO
{
    public class ScopusEntryDTO
    {
        [JsonPropertyName("@_fa")]
        public string Fa { get; set; }

        [JsonPropertyName("link")]
        public List<ScopusLinkDTO> Links { get; set; }

        [JsonPropertyName("prism:url")]
        public string URL { get; set; }

        [JsonPropertyName("dc:identifier")]
        public string Identifier { get; set; }

        [JsonPropertyName("eid")]
        public string EId { get; set; }

        [JsonPropertyName("dc:title")]
        public string Title { get; set; }

        [JsonPropertyName("dc:description")]
        public string Abstract { get; set; }

        [JsonPropertyName("dc:authkeywords")]
        public string Keywords { get; set; }

        [JsonPropertyName("dc:creator")]
        public string Creator{ get; set; }

        [JsonPropertyName("prism:publicationName")]
        public string PublicationName { get; set; }
        
        [JsonPropertyName("prism:isbn")]
        public List<ScopusISBNDTO> ISBN { get; set; }
        
        [JsonPropertyName("prism:issn")]
        public string ISSN { get; set; }

        [JsonPropertyName("prism:eIssn")]
        public string EISSN { get; set; }

        [JsonPropertyName("prism:volume")]
        public string Volume { get; set; }

        [JsonPropertyName("prism:issueIdentifier")]
        public string IssueIdentifier { get; set; }

        [JsonPropertyName("prism:pageRange")]
        public string PageRange { get; set; }

        [JsonPropertyName("prism:coverDate")]
        public string CoverDate { get; set; }

        [JsonPropertyName("prism:coverDisplayDate")]
        public string CoverDisplayDate { get; set; }

        [JsonPropertyName("prism:doi")]
        public string DOI { get; set; }

        [JsonPropertyName("citedby-count")]
        public string CitedByCount { get; set; }

        [JsonPropertyName("affiliation")]
        public List<ScopusAffiliationDTO> Affiliation { get; set; }

        [JsonPropertyName("prism:aggregationType")]
        public string AggregationType { get; set; }

        [JsonPropertyName("subtype")]
        public string SubType { get; set; }

        [JsonPropertyName("subtypeDescription")]
        public string SubTypeDescription { get; set; }

        [JsonPropertyName("source-id")]
        public string SourceId { get; set; }

        [JsonPropertyName("openaccess")]
        public string OpenAccess { get; set; }

        [JsonPropertyName("openaccessFlag")]
        public bool OpenAccessFlag { get; set; }

        [JsonPropertyName("freetoread")]
        public ScopusConfigContainerDTO FreeToRead { get; set; }

        [JsonPropertyName("freetoreadLabel")]
        public ScopusConfigContainerDTO FreeToReadLabel { get; set; }
    }
}
