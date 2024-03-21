using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Classes.Models
{
    public class SearchParameters
    {
        [Required(AllowEmptyStrings = false)]
        public string query { get; set; }
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
        [Range(0, int.MaxValue)]
        public int ReturnQuantityLimit { get; set; } = 0; // default = 0 = ilimitado
    }
}
