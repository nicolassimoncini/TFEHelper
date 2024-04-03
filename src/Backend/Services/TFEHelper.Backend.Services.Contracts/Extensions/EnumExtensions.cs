using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Services.Contracts.DTO;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Configuration;
using TFEHelper.Backend.Tools.Enums;

namespace TFEHelper.Backend.Services.Contracts.Extensions
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
