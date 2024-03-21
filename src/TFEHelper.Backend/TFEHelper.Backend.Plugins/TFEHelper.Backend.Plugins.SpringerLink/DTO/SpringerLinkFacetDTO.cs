using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.SpringerLink.DTO
{
    public class SpringerLinkFacetDTO
    {
        public string Name { get; set; }
        public List<SpringerLinkFacetValueDTO> Values { get; set; }
    }
}