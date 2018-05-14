using System.Linq;

namespace Frapid.WebsiteBuilder
{
    public static class StringExtension
    {
        public static string Truncate(this string input, int limit = 50)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            if (input.Length > limit)
            {
                int cutPos = new string(input.Take(limit).ToArray()).LastIndexOf(' ');
                string result = new string(input.Take(cutPos).ToArray());

                return result + " ...";
            }


            return input;
        }
    }
}