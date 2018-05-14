using System;

namespace Frapid.Framework.Extensions
{
    public static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var investigator = new AttributeValueInvestigator();
            return investigator.Investigate(type, valueSelector);
        }
    }
}