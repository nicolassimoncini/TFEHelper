using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace TFEHelper.Backend.Core.API.SpringerLink.DTO
{
    public class SpringerLinkRecordDTO
    {
        public string ContentType { get; set; }
        public string Identifier { get; set; }
        public string Language { get; set; }
        public List<SpringerLinkURLDTO> URL { get; set; }
        public string Title { get; set; }
        public List<SpringerLinkCreatorDTO> Creators { get; set; }
        public List<SpringerLinkBookEditorDTO> BookEditors { get; set; }
        public string PublicationName { get; set; }
        public string OpenAccess { get; set; }
        public string DOI { get; set; }
        public string Publisher { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublicationDate { get; set; }
        public string PublicationType { get; set; }
        public string PrintISBN { get; set; }
        public string ElectronicISBN { get; set; }
        public string ISBN { get; set; }
        [JsonIgnore]
        public List<string> Genre { get; set; }
        public DateTime OnlineDate { get; set; }
        public int SeriesId { get; set; }
        public string CopyRight { get; set; }
        public string Abstract { get; set; }
        public List<SpringerLinkConferenceInfoDTO> ConferenceInfo { get; set; }
        public List<string> Subjects { get; set; }
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.