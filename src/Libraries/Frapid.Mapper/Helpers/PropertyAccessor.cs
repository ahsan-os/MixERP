using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Frapid.Mapper.Helpers
{
    public static class PropertyAccessor
    {
        public static List<PropertyInfo> GetProperties(Type item)
        {
            string key = item.FullName;
            var properties = MapperCache.GetCache<List<PropertyInfo>>(key);

            if (properties != null)
            {
                return properties;
            }

            properties = item.GetProperties().Where(x => x.CanWrite).ToList();
            MapperCache.AddToCache(key, properties);

            return properties;
        }
    }
}