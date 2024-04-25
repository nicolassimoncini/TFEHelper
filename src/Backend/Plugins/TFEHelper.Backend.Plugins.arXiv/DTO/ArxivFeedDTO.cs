using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.arXiv.DTO
{
    internal class ArxivFeedDTO
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string Updated { get; set; }
        public string Link { get; set; }
        public int TotalResults { get; set; }
        public int StartIndex { get; set; }
        public int ItemsPerPage { get; set; }
        public List<ArxivEntryDTO> Entries { get; set; } = new List<ArxivEntryDTO>();
    }
}
