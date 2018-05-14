using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using Frapid.Mapper.Database;

namespace Frapid.Mapper.Helpers
{
    public static class ResultsetCache
    {
        private static string GetCacheKey(MapperDb db, DbCommand command)
        {
            string key = db.ConnectionString;
            key += "." + command.CommandText + ".";

            var dbParameters = (from DbParameter parameter in command.Parameters select parameter.Value.ToString()).ToList();
            key += "." + string.Join(",", dbParameters);
            return key;
        }

        public static Collection<ICollection<KeyValuePair<string, object>>> Get(MapperDb db, DbCommand command)
        {
            if (!db.CacheResults)
            {
                return null;
            }

            string key = GetCacheKey(db, command);
            var item = MapperCache.GetCache<Collection<ICollection<KeyValuePair<string, object>>>>(key);

            return item;
        }

        public static void Set(MapperDb db, DbCommand command, Collection<ICollection<KeyValuePair<string, object>>> item)
        {
            if (!db.CacheResults)
            {
                return;
            }

            string key = GetCacheKey(db, command);
            int cacheDuration = db.CacheMilliseconds;
            var expiresOn = DateTimeOffset.UtcNow.AddMilliseconds(cacheDuration);


            MapperCache.AddToCache(key, item, expiresOn);
        }
    }
}