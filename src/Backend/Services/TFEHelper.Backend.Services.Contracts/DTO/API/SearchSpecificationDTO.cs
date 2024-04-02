using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Services.Contracts.DTO.API
{
    public class SearchSpecificationDTO
    {
        public string Query { get; set; }
        public List<SearchParameterDTO> Parameters { get; set; } = new List<SearchParameterDTO>();
    }
}
