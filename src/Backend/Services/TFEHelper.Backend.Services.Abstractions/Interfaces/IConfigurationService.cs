using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Services.Contracts.DTO.Configuration;

namespace TFEHelper.Backend.Services.Abstractions.Interfaces
{
    public interface IConfigurationService
    {
        IEnumerable<EnumerationTableDTO> GetEnumerationTables();
    }
}
