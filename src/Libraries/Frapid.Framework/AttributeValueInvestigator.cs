using System;
using System.Linq;

namespace Frapid.Framework
{
    public sealed class AttributeValueInvestigator : IAttributeValueInvestigator
    {
        public TValue Investigate<TAttribute, TValue>(Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var attribute = type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            return attribute != null ? valueSelector(attribute) : default(TValue);
        }
    }
}