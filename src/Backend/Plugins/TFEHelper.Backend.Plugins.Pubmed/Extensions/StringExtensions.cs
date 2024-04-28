using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Pubmed.Extensions
{
    internal static class StringExtensions
    {
        public static string ConcatIfNotNull(this string a, string b)
        {
            if (b != null) return string.Concat(a, b);
            else return null;
        }
    }
}
