using System;
using System.Linq;

namespace Frapid.Messaging
{
    public sealed class EmailProcessor
    {
        public static IEmailProcessor GetDefault(string database)
        {
            var iType = typeof(IEmailProcessor);
            var members = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => iType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance);

            foreach (IEmailProcessor member in members)
            {
                member.InitializeConfig(database);
                if (member.IsEnabled)
                {
                    return member;
                }
            }

            return null;
        }

        public static IEmailConfig GetDefaultConfig(string database)
        {
            var processor = GetDefault(database);
            return processor.Config;
        }
    }
}