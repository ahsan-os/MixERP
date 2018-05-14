using System.IO;
using System.Text;
using System.Web.Hosting;
using System.Xml;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    public sealed class ThemeCreator
    {
        public ThemeCreator(ThemeInfo info)
        {
            this.Info = info;
        }

        public string ThemeDirectory { get; private set; }
        public string ConfigFileLocation { get; private set; }
        public ThemeInfo Info { get; }

        private void CreateDirectory(string tenant)
        {
            string directory = $"~/Tenants/{tenant}/Areas/Frapid.WebsiteBuilder/Themes/{this.Info.ThemeName}";
            directory = HostingEnvironment.MapPath(directory);

            this.ThemeDirectory = directory;

            if (directory == null)
            {
                throw new ThemeCreateException(Resources.CouldNotCreateThemeInvalidDestinationDirectory);
            }

            if (Directory.Exists(directory))
            {
                throw new ThemeCreateException(Resources.CouldNotCreateThemeDestinationDirectoryAlreadyExists);
            }

            Directory.CreateDirectory(directory);
        }

        private void AddKey(XmlTextWriter xml, string key, string value)
        {
            xml.WriteStartElement("add");
            xml.WriteAttributeString("key", key);
            xml.WriteAttributeString("value", value);
            xml.WriteEndElement(); //add
        }

        private void CreateConfig()
        {
            this.ConfigFileLocation = Path.Combine(this.ThemeDirectory, "Theme.config");

            using (var xml = new XmlTextWriter(this.ConfigFileLocation, Encoding.UTF8))
            {
                xml.WriteStartDocument(true);
                xml.Formatting = Formatting.Indented;
                xml.Indentation = 4;

                xml.WriteStartElement("configuration");
                xml.WriteStartElement("appSettings");

                this.AddKey(xml, "ThemeName", this.Info.ThemeName);
                this.AddKey(xml, "Author", this.Info.Author);
                this.AddKey(xml, "AuthorUrl", this.Info.AuthorUrl);
                this.AddKey(xml, "AuthorEmail", this.Info.AuthorEmail);
                this.AddKey(xml, "ConvertedBy", this.Info.ConvertedBy);
                this.AddKey(xml, "ReleasedOn", this.Info.ReleasedOn);
                this.AddKey(xml, "Version", this.Info.Version);
                this.AddKey(xml, "Category", this.Info.Category);
                this.AddKey(xml, "Responsive", this.Info.Responsive ? "Yes" : "No");
                this.AddKey(xml, "Framework", this.Info.Framework);
                this.AddKey(xml, "Tags", string.Join(",", this.Info.Tags));
                this.AddKey(xml, "HomepageLayout", this.Info.HomepageLayout);
                this.AddKey(xml, "DefaultLayout", this.Info.DefaultLayout);

                xml.WriteEndElement(); //appSettings
                xml.WriteEndElement(); //configuration
                xml.Close();
            }
        }

        public void Create(string tenant)
        {
            this.CreateDirectory(tenant);
            this.CreateConfig();
        }
    }
}