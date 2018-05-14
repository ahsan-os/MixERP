using System.ComponentModel.DataAnnotations;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    public sealed class ThemeInfo
    {
        [Required]
        public string ThemeName { get; set; }

        [Required]
        public string Author { get; set; }

        public string AuthorUrl { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string AuthorEmail { get; set; }

        public string ConvertedBy { get; set; }
        public string ReleasedOn { get; set; }
        public string Version { get; set; }
        public string Category { get; set; }
        public bool Responsive { get; set; }
        public string Framework { get; set; }
        public string[] Tags { get; set; }
        public string HomepageLayout { get; set; }
        public string DefaultLayout { get; set; }
        public bool IsValid { get; set; }
    }
}