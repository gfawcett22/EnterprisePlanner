using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WebApiHelpersTypes.Helpers
{
    public static class ObjectToDictionaryConverter
    {
        public static Dictionary<string, string> ConvertToDictionary (object obj)
        {
            return (from x in obj.GetType().GetTypeInfo().DeclaredProperties select x).ToDictionary(x => x.Name, x => (x.GetMethod.Invoke(obj, null) == null ? "" : x.GetMethod.Invoke(obj, null).ToString()));
        }
    }
}
