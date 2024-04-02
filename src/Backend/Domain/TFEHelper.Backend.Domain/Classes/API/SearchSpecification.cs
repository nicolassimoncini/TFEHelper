using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Classes.API
{
    public class SearchSpecification
    {
        public string Query { get; set; } = string.Empty;
        public List<SearchParameter> Parameters { get; set; } = new List<SearchParameter>();
    }
}
