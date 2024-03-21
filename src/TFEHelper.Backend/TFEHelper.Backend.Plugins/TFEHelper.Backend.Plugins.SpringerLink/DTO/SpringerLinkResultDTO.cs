using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.SpringerLink.DTO
{
    public class SpringerLinkResultDTO
    {
        public int Total { get; set; }
        public int Start { get; set; }
        public int PageLength { get; set; }
        public int RecordsDisplayed { get; set; }
    }
}