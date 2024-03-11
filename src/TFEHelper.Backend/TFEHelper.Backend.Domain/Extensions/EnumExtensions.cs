using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
