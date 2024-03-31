using System.Collections.Generic;

namespace TFEHelper.Backend.Plugins.SpringerLink.DTO
{
    public class SpringerLinkRootDTO
    {
        public string APIMessage { get; set; }
        public string Query { get; set; }
        public string APIKey { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
        public List<SpringerLinkResultDTO> Result { get; set; }
        public List<SpringerLinkRecordDTO> Records { get; set; }
        public List<SpringerLinkFacetDTO> Facets { get; set; }
    }
}
