using System;

namespace Frapid.Areas.Caching
{
    public interface ICacheKeyGenerator
    {
        Uri Url { get; set; }

        string Generate();
    }
}