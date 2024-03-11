using System.Collections.Generic;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace TFEHelper.Backend.Core.API.SpringerLink.DTO
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

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
