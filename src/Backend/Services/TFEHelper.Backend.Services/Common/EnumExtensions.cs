using TFEHelper.Backend.Services.Contracts.DTO.Configuration;
using TFEHelper.Backend.Tools.Enums;

namespace TFEHelper.Backend.Services.Common
{
    public static class EnumExtensions
    {
        public static class Transformer<T> where T : struct, Enum
        {
            public static EnumerationTableDTO ToEnumerationDTO()
            {
                EnumerationTableDTO result = new EnumerationTableDTO();
                result.Name = typeof(T).Name;

                foreach (var enumValue in (Enum.GetValues(typeof(T))))
                {
                    result.Items.Add(new EnumerationTableItemDTO()
                    {
                        Name = ((T)enumValue).GetDisplayName(),
                        Value = (int)enumValue
                    });
                }
                return result;
            }
        }
    }
}
