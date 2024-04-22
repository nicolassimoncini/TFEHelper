using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.PluginBase.Common.Classes
{
    public static class NamedKeyValuePairHelper
    {
        private static NamedKeyValuePair<T> ValuePairConverter<T>(NamedKeyValuePair<object> origin)
        {
            return new NamedKeyValuePair<T>()
            {
                Name = origin.Name,
                Value = (T)Convert.ChangeType(origin.Value, typeof(T))
            };
        }

        public static void Add(this IList<NamedKeyValuePair<object>> list, string name, object value)
        {
            list.Add(new NamedKeyValuePair<object>() { Name = name, Value = value });
        }

        public static T Get<T>(this IList<NamedKeyValuePair<object>> list, string parameterName)
        {
            var value = list.FirstOrDefault(x => x.Name == parameterName)?.Value;
            return (value != null) ? (T)Convert.ChangeType(value, typeof(T)) : default;
        }

        public static void Add(this IList<NamedKeyValuePair<IList<NamedKeyValuePair<object>>>> list, string collectionName, string name, object value)
        {
            var _list = list.FirstOrDefault(x => x.Name == collectionName);
            if (_list == null)
            {
                list.Add(new NamedKeyValuePair<IList<NamedKeyValuePair<object>>>() { Name = collectionName, Value = new List<NamedKeyValuePair<object>>() });
            }
            list.First(x => x.Name == collectionName).Value.Add(new NamedKeyValuePair<object>() { Name = name, Value = value });
        }

        public static IList<NamedKeyValuePair<T>> Get<T>(this IList<NamedKeyValuePair<IList<NamedKeyValuePair<object>>>> list, string parameterName)
        {
            var _list = list.FirstOrDefault(x => x.Name == parameterName)?.Value;
            return _list.ToList().ConvertAll(new Converter<NamedKeyValuePair<object>, NamedKeyValuePair<T>>(ValuePairConverter<T>));
        }
    }
}
