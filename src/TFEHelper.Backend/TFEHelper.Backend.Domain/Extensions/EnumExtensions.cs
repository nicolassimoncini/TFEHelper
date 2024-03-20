using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using TFEHelper.Backend.Domain.Classes.DTO;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Enums;

namespace TFEHelper.Backend.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static BibTeXPublicationType ToPublicationType(this string str)
        {
            if (!Enum.TryParse(str, true, out BibTeXPublicationType pt))
                return BibTeXPublicationType.Misc;
            else return pt;
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
              .GetMember(enumValue.ToString())
              .First()
              .GetCustomAttribute<DisplayAttribute>()
              ?.GetName() ?? enumValue.ToString();
        }

        public static class Transformer<T> where T : struct, Enum
        {
            public static EnumerationTable ToEnumerationDTO()
            {
                EnumerationTable result = new EnumerationTable();
                result.Name = typeof(T).Name;

                foreach (var enumValue in (Enum.GetValues(typeof(T))))
                {
                    result.Items.Add(new EnumerationTableItem()
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