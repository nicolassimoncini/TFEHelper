using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Classes.API;

namespace TFEHelper.Backend.Core.Configuration.Interfaces
{
    public interface ITFEHelperConfigurationManager
    {
        IEnumerable<EnumerationTable> GetEnumerationTables();
    }
}
