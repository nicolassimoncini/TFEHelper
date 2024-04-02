using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Classes.Plugin
{
    public class PublicationsCollectorParameters
    {
        [Required(AllowEmptyStrings = false)]
        public string Query { get; set; } = string.Empty;
        public string SearchIn { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
        [Range(0, int.MaxValue)]
        public int ReturnQuantityLimit { get; set; } = 0; // default = 0 = ilimitado
    }
}