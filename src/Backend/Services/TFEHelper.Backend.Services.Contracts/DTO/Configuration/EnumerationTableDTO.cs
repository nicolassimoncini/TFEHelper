using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Services.Contracts.DTO.Configuration
{
    public class EnumerationTableDTO
    {
        public string Name { get; set; }
        public List<EnumerationTableItemDTO> Items { get; set; } = new List<EnumerationTableItemDTO>();
    }
}
