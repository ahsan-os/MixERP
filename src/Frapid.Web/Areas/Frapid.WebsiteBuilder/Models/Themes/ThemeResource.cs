using System.Collections.Generic;
using System.IO;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    public sealed class ThemeResource
    {
        public ThemeResource()
        {
            this.Children = new List<ThemeResource>();
        }

        public string Text { get; set; }
        public string Path { get; set; }
        public bool IsDirectory { get; set; }
        public string Icon { get; set; }
        public List<ThemeResource> Children { get; }

        public void AddChild(ThemeResource resource)
        {
            if (resource.IsDirectory)
            {
                resource = Get(resource.Path);
            }

            this.Children.Add(resource);
        }

        public static ThemeResource NormalizePath(string root, ThemeResource resource)
        {
            resource.Path = resource.Path.Replace(root, "").Replace(@"\", @"/");

            foreach (var child in resource.Children)
            {
                child.Path = child.Path.Replace(root, "").Replace(@"\", @"/");
                if (child.IsDirectory)
                {
                    NormalizePath(root, child);
                }
            }

            return resource;
        }

        public static ThemeResource Get(string path)
        {
            var directory = new DirectoryInfo(path);

            var resource = new ThemeResource
            {
                Path = directory.FullName,
                Text = directory.Name,
                IsDirectory = true
            };

            foreach (var child in directory.GetFiles())
            {
                resource.AddChild(new ThemeResource
                {
                    Path = child.FullName,
                    Text = child.Name,
                    IsDirectory = false,
                    Icon = "file text outline icon"
                });
            }

            foreach (var child in directory.GetDirectories())
            {
                resource.AddChild(new ThemeResource
                {
                    Path = child.FullName,
                    Text = child.Name,
                    IsDirectory = true,
                    Icon = "folder open outline icon"
                });
            }

            return resource;
        }
    }
}