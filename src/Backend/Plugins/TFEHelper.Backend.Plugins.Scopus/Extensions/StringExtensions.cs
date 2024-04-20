using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Enums;

namespace TFEHelper.Backend.Plugins.Scopus.Extensions
{
    internal static class StringExtensions
    {
        public static string NormalizePublicationType(string pt) => pt switch
        {
            "Chapter ConferencePaper" => BibTeXPublicationPLGType.Conference.ToString(),
            "Chapter" => BibTeXPublicationPLGType.Book.ToString(),
            _ => pt
        };
    }
}
