using System;

namespace Frapid.Framework
{
    public sealed class UrlCombiner : IUrlCombiner
    {
        public string Combine(string domain, string path)
        {
            try
            {
                Uri result;

                if (Uri.TryCreate(new Uri(domain), path, out result))
                {
                    return result.ToString();
                }
            }
            catch (Exception)
            {
                //
            }
            string url = domain + "/" + path;
            return Uri.EscapeUriString(url);
        }
    }
}