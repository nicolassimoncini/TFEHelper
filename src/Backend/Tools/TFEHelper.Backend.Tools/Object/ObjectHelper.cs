using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Tools.Object
{
    public static class ObjectHelper
    {
        public static bool Implements(this object obj, Type type)
        {
            Type tt = type;
            Type to = obj.GetType();

            if (tt.IsInterface && tt.IsGenericType)
            {
                return to
                    .GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == tt);
            }
            else return to.IsAssignableTo(tt);
        }

        public static object? GetPropertyValue(this object source, string name)
        {
            return source.GetType()?.GetProperty(name)?.GetValue(source, null);
        }
    }
}
