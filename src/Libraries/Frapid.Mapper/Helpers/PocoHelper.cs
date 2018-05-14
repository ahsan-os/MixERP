using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Frapid.Mapper.Decorators;
using Frapid.Mapper.Extensions;

namespace Frapid.Mapper.Helpers
{
    public static class PocoHelper
    {
        public static string GetTableName<T>(T poco)
        {
            return poco.GetType().GetAttributeValue((TableNameAttribute attribute) => attribute.TableName);
        }

        public static PrimaryKeyAttribute GetPrimaryKey<T>(T poco)
        {
            return poco.GetType().GetAttributeValue((PrimaryKeyAttribute attribute) => attribute);
        }

        public static IEnumerable<string> GetIgnoredColumns<T>(this T source)
        {
            var expandoObject = source as ExpandoObject;

            if (expandoObject != null)
            {
                return new List<string>();
            }

            return source.GetType()
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(IgnoreAttribute))).Select(x=>x.Name);
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            var expandoObject = source as ExpandoObject;
            var ignored = source.GetIgnoredColumns();

            if (expandoObject != null)
            {
                return (IDictionary<string, object>) source;
            }

            return source.GetType().GetProperties(bindingAttr).Where(x => !ignored.Contains(x.Name)).ToDictionary
                (
                    propInfo => propInfo.Name,
                    propInfo => propInfo.GetValue(source, null)
                );
        }

        public static void UpdateProperty<T>(this T source, string propertyName, object value)
        {
            
        }
    }
}