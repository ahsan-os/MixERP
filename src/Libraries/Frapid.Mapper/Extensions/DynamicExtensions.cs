using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using Frapid.Mapper.Helpers;

namespace Frapid.Mapper.Extensions
{
    public static class DynamicExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static IEnumerable<T> ToObject<T>(this ICollection<ICollection<KeyValuePair<string, object>>> list) where T : new()
        {
            var type = typeof(T);
            var retVal = new List<T>();

            if (type == typeof(object))
            {
                foreach (var item in list)
                {
                    var expando = new ExpandoObject();
                    var collection = (ICollection<KeyValuePair<string, object>>) expando;

                    collection.AddRange(item);

                    dynamic dynamic = expando;
                    retVal.Add(dynamic);
                }

                return retVal;
            }


            var allKeys = new Collection<string>();

            var props = PropertyAccessor.GetProperties(type);

            foreach (var property in props)
            {
                var dictionary = list.FirstOrDefault();

                string key = dictionary?.FirstOrDefault(x => x.Key.Equals(property.Name, StringComparison.OrdinalIgnoreCase)).Key;

                if (!string.IsNullOrWhiteSpace(key))
                {
                    allKeys.Add(key);
                }
            }


            foreach (var collection in list)
            {
                var instance = New<T>.Instance();

                foreach (string key in allKeys)
                {
                    var property = props.FirstOrDefault(x => x.Name.Equals(key));
                    var propertyValue = collection.FirstOrDefault(x => x.Key.Equals(key)).Value;


                    if (property == null || propertyValue == null)
                    {
                        continue;
                    }

                    var value = TypeConverter.Convert(propertyValue, property.PropertyType);
                    property.SetValue(instance, value);
                }

                retVal.Add(instance);
            }

            return retVal;
        }
    }
}