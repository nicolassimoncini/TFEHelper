using System.Reflection;
using TFEHelper.Backend.Core.Configuration.Interfaces;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Domain.Extensions;

namespace TFEHelper.Backend.Core.Configuration.Implementations
{
    public class TFEHelperConfigurationManager : ITFEHelperConfigurationManager
    {
        public TFEHelperConfigurationManager()
        {
        }

        public IEnumerable<EnumerationTable> GetEnumerationTables()
        {
            List<EnumerationTable> result = new List<EnumerationTable>();
            result.Add(EnumExtensions.Transformer<BibTeXPublicationType>.ToEnumerationDTO());
            result.Add(EnumExtensions.Transformer<FileFormatType>.ToEnumerationDTO());
            result.Add(EnumExtensions.Transformer<SearchSourceType>.ToEnumerationDTO());
            return result;
        }
    }
}
