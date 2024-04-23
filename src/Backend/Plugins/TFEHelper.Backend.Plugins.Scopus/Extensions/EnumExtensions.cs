using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Enums;

namespace TFEHelper.Backend.Plugins.Scopus.Extensions
{
    internal static class EnumExtensions
    {
        public static BibTeXPublicationPLGType ToPublicationType(this string str)
        {
            if (!Enum.TryParse(str, true, out BibTeXPublicationPLGType pt))
                return BibTeXPublicationPLGType.Misc;
            else return pt;
        }
    }
}
