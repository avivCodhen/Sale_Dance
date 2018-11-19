using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Utility
{
    public static class ReflectionExtension
    {
        public static string GetPropertyValue<T>(this T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();
        }
    }
}
