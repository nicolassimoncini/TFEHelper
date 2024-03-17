using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Tools.Assembly
{
    public static class AssemblyHelper
    {
        public static IEnumerable<T> GetAllImplementersOf<T>(System.Reflection.Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(type => typeof(T).IsAssignableFrom(type)
                    && type.IsClass
                    && !type.IsAbstract
                    && !type.IsGenericType
                    && type.GetConstructor(new Type[0]) != null)
                .Select(type => (T)Activator.CreateInstance(type)!)
                .ToList();
        }
    }
}