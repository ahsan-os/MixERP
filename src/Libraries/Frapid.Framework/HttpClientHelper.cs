using System.Net.Http;

namespace Frapid.Framework
{
    public static class GlobalHttpClient
    {
        private static HttpClient _client;

        public static HttpClient Get()
        {
            if (_client != null)
            {
                return _client;
            }

            _client = new HttpClient();
            _client.DefaultRequestHeaders.ConnectionClose = true;

            return _client;
        }
    }
}