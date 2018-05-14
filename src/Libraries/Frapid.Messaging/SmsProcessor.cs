using System;
using System.Linq;

namespace Frapid.Messaging
{
    public sealed class SmsProcessor
    {
        public static ISmsProcessor GetDefault(string database)
        {
            var iType = typeof(ISmsProcessor);
            var members = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => iType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance);

            foreach (ISmsProcessor member in members)
            {
                member.InitializeConfig(database);
                if (member.IsEnabled)
                {
                    return member;
                }
            }

            return null;
        }

        public static ISmsConfig GetDefaultConfig(string database)
        {
            var processor = GetDefault(database);
            return processor.Config;
        }
    }
}