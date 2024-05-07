using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Services.Contracts.Attributes;

namespace TFEHelper.Backend.Services.Contracts.DTO.Plugin
{
    public class PublicationsCollectorParametersDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Query { get; set; }
        
        public string SearchIn { get; set; }
        
        public string Subject { get; set; }

        [Required(AllowEmptyStrings = false)]        
        public DateOnly DateFrom { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DateGreaterOrEqualsThan("DateFrom")]
        public DateOnly DateTo { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(0, 2000)]
        public int ReturnQuantityLimit { get; set; } = 0;
    }
}