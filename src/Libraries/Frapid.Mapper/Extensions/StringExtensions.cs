using Frapid.Mapper.Helpers;
using Humanizer;

namespace Frapid.Mapper.Extensions
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string name)
        {
            var converter = new TitleCaseConverter();
            return converter.Convert(name);
        }

        public static string ToUnderscoreCase(this string name)
        {
            var converter = new UnderscoreCaseConverter();
            return converter.Convert(name);
        }

        public static string ToUnderscoreLowerCase(this string name)
        {
            return ToUnderscoreCase(name).ToLower();
        }

        public static string ToSentence(this string name)
        {
            string converted = name.Humanize(LetterCasing.Sentence);
            return converted;
        }


        public static string ToTitleCaseSentence(this string name)
        {
            string converted = name.Humanize(LetterCasing.Title);
            return converted;
        }

        public static string ToPascalCase(this string name)
        {
            var titleCase = new TitleCaseConverter();
            var pascalCase = new PascalCaseConverter(titleCase);
            return pascalCase.Convert(name);
        }
    }
}