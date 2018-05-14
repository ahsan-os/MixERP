using System;

namespace Frapid.Framework
{
    public interface IAttributeValueInvestigator
    {
        TValue Investigate<TAttribute, TValue>(Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute;
    }
}