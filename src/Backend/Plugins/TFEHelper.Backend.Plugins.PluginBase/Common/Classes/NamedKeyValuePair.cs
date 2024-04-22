using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.PluginBase.Common.Classes
{
    public class NamedKeyValuePair<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }
    }
}
