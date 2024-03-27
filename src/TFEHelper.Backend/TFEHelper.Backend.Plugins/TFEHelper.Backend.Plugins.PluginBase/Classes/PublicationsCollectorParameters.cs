using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.PluginBase.Classes
{
    public class PublicationsCollectorParameters
    {
        [Required(AllowEmptyStrings = false)]
        public string Query { get; set; }
        public string SearchIn { get; set; }
        public string Subject { get; set; }
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
        [Range(0, int.MaxValue)]
        public int ReturnQuantityLimit { get; set; } = 0; // default = ilimitado
    }
}
