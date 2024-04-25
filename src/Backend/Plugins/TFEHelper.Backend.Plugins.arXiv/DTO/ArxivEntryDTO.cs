using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.arXiv.DTO
{
    internal class ArxivEntryDTO
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public DateTime Published { get; set; }
        public DateTime Updated { get; set; }
        public string Summary { get; set; }
        public List<ArxivAuthorDTO> Authors { get; set; }
        public List<ArxivLinkDTO> Links { get; set; }
        public List<ArxivCategoryDTO> Categories { get; set; }
        public ArxivCategoryDTO PrimaryCateogory { get; set; }
        public string Comment { get; set; }
        public string JournalRefference { get; set; }
        public string DOI { get; set; }
    }
}
