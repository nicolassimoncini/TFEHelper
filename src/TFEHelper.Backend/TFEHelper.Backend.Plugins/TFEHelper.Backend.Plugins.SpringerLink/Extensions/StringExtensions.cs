using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Enums;

namespace TFEHelper.Backend.Plugins.SpringerLink.Extensions
{
    internal static class StringExtensions
    {
        public static string NormalizePublicationType(string pt) => pt switch
        {
            "Chapter ConferencePaper" => BibTeXPublicationType.Conference.ToString(),
            "Chapter" => BibTeXPublicationType.Book.ToString(),
            _ => pt
        };
    }
}
