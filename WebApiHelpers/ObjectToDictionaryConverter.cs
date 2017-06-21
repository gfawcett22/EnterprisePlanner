using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WebApiHelpers
{
    public static class ObjectToDictionaryConverter
    {
        public static Dictionary<string, string> ConvertToDictionary (object obj)
        {
            return (from x in obj.GetType().GetProperties() select x).ToDictionary(x => x.Name, x => (x.GetGetMethod().Invoke(obj, null) == null ? "" : x.GetGetMethod().Invoke(obj, null).ToString()));
        }
    }
}
