using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WebApiHelpersTypes.Helpers
{
    public static class ObjectToDictionaryConverter
    {
        public static Dictionary<string, string> ConvertToDictionary(object obj)
        {
            var dict = new Dictionary<string, string>();
            foreach (var x in obj.GetType().GetTypeInfo().GetAllProperties())
            {
                var propValue = x.GetMethod.Invoke(obj, null) == null ? "" : x.GetMethod.Invoke(obj, null).ToString();
                //only add if the value for that property isnt empty
                if(!string.IsNullOrEmpty(propValue))
                    dict.Add(x.Name, propValue);
            }
            return dict;
        }

        public static IEnumerable<PropertyInfo> GetAllProperties(this TypeInfo typeInfo) => GetAll(typeInfo, ti => ti.DeclaredProperties);

        private static IEnumerable<T> GetAll<T>(TypeInfo typeInfo, Func<TypeInfo, IEnumerable<T>> accessor)
        {
            while (typeInfo != null)
            {
                foreach (var t in accessor(typeInfo))
                {
                    yield return t;
                }

                typeInfo = typeInfo.BaseType?.GetTypeInfo();
            }
        }
    }
}
