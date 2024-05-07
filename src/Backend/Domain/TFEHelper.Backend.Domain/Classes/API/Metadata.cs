using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Classes.API
{
    public class Metadata
    {
        public int TotalPages { get; set; } 
        public int PageSize { get; set; }
        public int PageId { get; set; }
        public int TotalCount { get; set; } 
    }
}