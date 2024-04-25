using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.arXiv.Extensions
{
    internal static class ListExtensions
    {
        /// <summary>
        /// Convierte una lista de elementos de tipo <typeparamref name="T"/> en un <see cref="string"/> utilizando <paramref name="token"/> como separador y utilizando la función <paramref name="converter"/> pasada por parámetro para convertir cada elemento.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="token"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public static string ToString<T>(this IEnumerable<T> list, string token, Func<T, string> converter)
        {
            return string.Join(token, list.Select(converter));
        }
    }
}
