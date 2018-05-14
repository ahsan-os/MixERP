using System.IO;
using Frapid.Configuration;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    public static class ThemeInfoParser
    {
        private static string Get(string path, string key)
        {
            return ConfigurationManager.ReadConfigurationValue(path, key);
        }

        public static ThemeInfo Parse(string path)
        {
            var theme = new ThemeInfo();

            if (path == null || !File.Exists(path))
            {
                theme.IsValid = false;
            }

            theme.ThemeName = Get(path, "ThemeName");
            theme.Author = Get(path, "Author");
            theme.AuthorEmail = Get(path, "AuthorEmail");
            theme.ConvertedBy = Get(path, "ConvertedBy");
            theme.ReleasedOn = Get(path, "ReleasedOn");
            theme.Version = Get(path, "Version");
            theme.Category = Get(path, "Category");
            theme.Responsive = Get(path, "Responsive").ToUpperInvariant().Equals("TRUE");
            theme.Framework = Get(path, "Framework");
            theme.Tags = Get(path, "Tags").Split(',');
            theme.HomepageLayout = Get(path, "HomepageLayout");
            theme.DefaultLayout = Get(path, "DefaultLayout");

            string directory = Path.GetDirectoryName(path);

            if (directory == null || string.IsNullOrWhiteSpace(theme.ThemeName))
            {
                theme.IsValid = false;
                return theme;
            }

            if (!File.Exists(Path.Combine(directory, theme.HomepageLayout)))
            {
                throw new ThemeInfoException(Resources.InvalidThemeInvalidHomepageLayoutPath);
            }

            if (!File.Exists(Path.Combine(directory, theme.DefaultLayout)))
            {
                throw new ThemeInfoException(Resources.InvalidThemeInvalidDefaultLayoutPath);
            }

            theme.IsValid = true;

            return theme;
        }
    }
}