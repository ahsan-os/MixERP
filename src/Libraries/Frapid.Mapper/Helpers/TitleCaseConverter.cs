using Humanizer;

namespace Frapid.Mapper.Helpers
{
    public sealed class TitleCaseConverter
    {
        public string Convert(string name)
        {
            string converted = name.Humanize(LetterCasing.Title).Replace(" ", "");
            return converted;
        }
    }
}