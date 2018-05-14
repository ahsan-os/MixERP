using System;

namespace Frapid.Mapper.Decorators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAttribute : Attribute
    {
    }
}