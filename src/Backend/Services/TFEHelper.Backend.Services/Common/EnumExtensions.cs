using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Configuration;
using TFEHelper.Backend.Tools.Enums;

namespace TFEHelper.Backend.Services.Common
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Transform an <see cref="enum"/> to a <see cref="EnumerationTableDTO"/> structure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
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

        /// <summary>
        /// Returns file extension without "." related to a determined <see cref="FileFormatType"/>.
        /// </summary>
        /// <param name="formatType">The format type</param>
        /// <returns></returns>
        public static string ToFileNameExtension(this FileFormatType formatType)
        {
            return formatType switch
            {
                FileFormatType.CSV => "csv",
                FileFormatType.BibTeX => "bib",
                _ => "txt"
            };
        }
    }
}
