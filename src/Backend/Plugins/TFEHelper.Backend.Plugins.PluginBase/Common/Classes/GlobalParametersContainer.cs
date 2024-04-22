using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.PluginBase.Common.Classes
{
    public class GlobalParametersContainer
    {
        public IList<NamedKeyValuePair<object>> SingleValued { get { return _singleValued; } }
        public IList<NamedKeyValuePair<IList<NamedKeyValuePair<object>>>> CollectionValued { get { return _collectionValued; } }

        private readonly List<NamedKeyValuePair<object>> _singleValued;
        private readonly List<NamedKeyValuePair<IList<NamedKeyValuePair<object>>>> _collectionValued;

        public GlobalParametersContainer()
        {
            _singleValued = new();
            _collectionValued = new();
        }
    }
}
