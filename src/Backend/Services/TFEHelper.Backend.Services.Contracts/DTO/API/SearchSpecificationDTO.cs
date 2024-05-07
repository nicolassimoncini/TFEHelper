using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Services.Contracts.DTO.API
{
    public class SearchSpecificationDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Query { get; set; }
        public IEnumerable<SearchParameterDTO> Parameters { get; set; } = new List<SearchParameterDTO>();
        public IEnumerable<NarrowingExpressionDTO> Narrowings { get; set; } = new List<NarrowingExpressionDTO>();
    }
}
