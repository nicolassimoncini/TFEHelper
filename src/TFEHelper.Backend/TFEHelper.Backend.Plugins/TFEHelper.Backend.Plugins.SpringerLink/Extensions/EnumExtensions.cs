using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Enums;

namespace TFEHelper.Backend.Plugins.SpringerLink.Extensions
{
    internal static class EnumExtensions
    {
        public static BibTeXPublicationType ToPublicationType(this string str)
        {
            if (!Enum.TryParse(str, true, out BibTeXPublicationType pt))
                return BibTeXPublicationType.Misc;
            else return pt;
        }
    }
}
