using System;
using System.Collections.Generic;

namespace Frapid.Framework.Extensions
{
    public static class DictionaryExtensions
    {
        public static void Merge(this Dictionary<string, string> target, Dictionary<string, string> source)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var element in source)
            {
                if (!target.ContainsKey(element.Key))
                {
                    target.Add(element.Key, element.Value);
                }
            }
        }
    }
}