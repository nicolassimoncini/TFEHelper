using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Quic;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Extensions;

namespace TFEHelper.Backend.Domain.Extensions
{
    public static class StringExtensions
    {
        private static readonly bool[] _enabledChars = Initialize();

        private static bool[] Initialize()
        {
            bool[] table = new bool[65536];
            for (char c = '0'; c <= '9'; c++) table[c] = true;
            for (char c = 'A'; c <= 'Z'; c++) table[c] = true;
            for (char c = 'a'; c <= 'z'; c++) table[c] = true;
            table[' '] = true;
            //table['.'] = true;
            table['_'] = true;
            return table;
        }

        private static string RemoveSpecialCharacters(this string str)
        {
            char[] buffer = new char[str.Length];
            int index = 0;
            foreach (char c in str)
            {
                if (_enabledChars[c])
                {
                    buffer[index] = c;
                    index++;
                }
            }
            return new string(buffer, 0, index);
        }

        /// <summary>
        /// Retorna la cantidad mínima de palabras entre <paramref name="word1"/> y <paramref name="word2"/>
        /// o -1 si no halla a <paramref name="word1"/>, o <paramref name="word2"/> o si ambas son iguales.
        /// <br><i>La búsqueda y comparación interna entre palabras es insensible al caso.</i></br>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns></returns>
        public static int MinDistanceBetweenWords(this string s, string word1, string word2)
        {
            if (word1 == word2) return -1;
            int posW1 = 0, posW2 = 0, max, min;
            int res = int.MaxValue;
            List<string> list = s.RemoveSpecialCharacters().Split(' ').ToList();

            while (posW1 != -1 && posW2 != -1)
            {
                posW1 = list.FindIndex(x => x.Equals(word1, StringComparison.InvariantCultureIgnoreCase));
                if (posW1 != -1)
                {
                    posW2 = list.FindIndex(x => x.Equals(word2, StringComparison.InvariantCultureIgnoreCase));
                    if (posW2 != -1)
                    {
                        (max, min) = posW1 > posW2 ? (posW1, posW2) : (posW2, posW1);
                        if (max - min < res) res = max - min - 1;
                        list.RemoveRange(0, min + 1);
                    }
                }
            }
            return res < int.MaxValue ? res : -1;
        }

        /// <summary>
        /// Convierte el valor <see cref="string"/> a una lista de elementos de tipo <typeparamref name="T"/> separando cada uno por <paramref name="token"/> y convitiendo cada elemento utilizando la función <paramref name="converter"/> pasada por parámetro.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="token"></param>
        /// <param name="Converter"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this string str, string[] tokens, Func<string, T> converter)
        {
            List<T> result = new List<T>();
            foreach (string s in str.Split(tokens, StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries))
            {
                result.Add(converter(s));
            }
            return result;
        }
    }
}
