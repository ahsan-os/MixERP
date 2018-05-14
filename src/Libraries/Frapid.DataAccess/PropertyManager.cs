using System;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace Frapid.DataAccess
{
    public static class PropertyManager
    {
        public static object GetPropertyValue(object target, string name)
        {
            var site = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(0, name, target.GetType(),
                    new[] {CSharpArgumentInfo.Create(0, null)}));
            return site.Target(site, target);
        }
    }
}