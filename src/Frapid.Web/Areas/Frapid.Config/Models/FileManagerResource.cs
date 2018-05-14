using System.Collections.Generic;
using System.IO;

namespace Frapid.Config.Models
{
    public sealed class FileManagerResource
    {
        public FileManagerResource()
        {
            this.Children = new List<FileManagerResource>();
        }

        public string Text { get; set; }
        public string Path { get; set; }
        public bool IsDirectory { get; set; }
        public string Icon { get; set; }
        public List<FileManagerResource> Children { get; }

        public void AddChild(FileManagerResource resource)
        {
            if (resource.IsDirectory)
            {
                resource = Get(resource.Path);
            }

            this.Children.Add(resource);
        }

        public static FileManagerResource NormalizePath(string root, FileManagerResource resource)
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

        public static FileManagerResource Get(string path, string extensions = "")
        {
            var directory = new DirectoryInfo(path);

            var resource = new FileManagerResource
            {
                Path = directory.FullName,
                Text = directory.Name,
                IsDirectory = true
            };

            var files = string.IsNullOrWhiteSpace(extensions) ? directory.GetFiles() : directory.GetFiles(extensions);

            foreach (var child in files)
            {
                resource.AddChild(new FileManagerResource
                {
                    Path = child.FullName,
                    Text = child.Name,
                    IsDirectory = false,
                    Icon = "file text outline icon"
                });
            }

            foreach (var child in directory.GetDirectories())
            {
                resource.AddChild(new FileManagerResource
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