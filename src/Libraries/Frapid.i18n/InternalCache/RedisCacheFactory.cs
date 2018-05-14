using System;
using System.Linq;
using Frapid.Configuration;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Frapid.i18n.InternalCache
{
    internal class RedisCacheFactory
    {
        public RedisCacheFactory()
        {
            var serializer = new NewtonsoftSerializer();
            var connection = GetConnection();
            this.Client = new StackExchangeRedisCacheClient(connection, serializer);
        }

        public ICacheClient Client { get; }

        public static ConnectionMultiplexer Redis { get; private set; }

        public bool Add<T>(string key, T value, DateTimeOffset expiresAt)
        {
            return this.Client.Add(key, value, expiresAt);
        }

        public void Remove(string key)
        {
            bool result = this.Client.SearchKeys(key).Any();

            if (result)
            {
                this.Client.Remove(key);
            }
        }

        public T Get<T>(string key) where T : class
        {
            return this.Client.Get<T>(key);
        }

        public static ConnectionMultiplexer GetConnection()
        {
            if (Redis != null)
            {
                return Redis;
            }

            string cs = RedisConnectionString.GetConnectionString();
            Redis = ConnectionMultiplexer.Connect(cs);
            Redis.PreserveAsyncOrder = false;

            return Redis;
        }
    }
}