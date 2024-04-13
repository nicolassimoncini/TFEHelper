using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Tools.Strings
{
    /// <summary>
    /// Set of routines for handling strings.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Converts a string to its MD5 representation
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetMD5(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);

            StringBuilder sb = new StringBuilder();
            MD5 hash = MD5.Create();
            bytes = hash.ComputeHash(bytes);
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
    }
}
