using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Tools.Files
{
    public static class FileHelper
    {
        /// <summary>
        /// Creates a file name based on <see cref="Guid"/> value and extension provided.
        /// </summary>
        /// <param name="fileExtension">The file extension</param>
        /// <returns></returns>
        public static string GetRandomFileName(string fileExtension)
        {
            return string.Format(@"{0}.{1}", Guid.NewGuid(), fileExtension);
        }
    }
}
