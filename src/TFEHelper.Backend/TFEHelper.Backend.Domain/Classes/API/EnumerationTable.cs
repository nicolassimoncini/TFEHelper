using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Classes.API
{
    public class EnumerationTable
    {
        public string Name { get; set; }
        public List<EnumerationTableItem> Items { get; set; } = new List<EnumerationTableItem>();
    }
}
