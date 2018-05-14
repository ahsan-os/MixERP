using Frapid.Framework.Extensions;
using Humanizer;

namespace Frapid.Mapper.Helpers
{
    public sealed class UnderscoreCaseConverter
    {
        public string Convert(string name)
        {
            string converted = name.Humanize(LetterCasing.Title).Or("").Replace(" ", "_").ToLowerInvariant();
            return converted;
        }
    }
}