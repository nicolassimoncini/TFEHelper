using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.arXiv.DTO
{
    internal class ArxivLinkDTO
    {
        public string HRef { get; set; }
        public string Title { get; set; }
        public string Rel { get; set; }
        public string Type { get; set; }
    }
}
