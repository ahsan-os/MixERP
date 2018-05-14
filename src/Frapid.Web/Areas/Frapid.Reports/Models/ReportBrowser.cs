using System.IO;
using Frapid.Config.Models;
using Frapid.Configuration;
using Frapid.Mapper.Extensions;

namespace Frapid.Reports.Models
{
    public static class ReportBrowser
    {
        public static FileManagerResource GetFiles(string tenant, string module)
        {
            string path = $"/Areas/{module}/Reports/";
            path = PathMapper.MapPath(path).Replace("/", "\\");

            if (!Directory.Exists(path))
            {
                throw new ReportBrowserException($"Cannot find the module {module}.");
            }


            return Discover(path, module);
        }

        private static FileManagerResource Discover(string path, string module)
        {
            var resource = FileManagerResource.Get(path, "*.xml");
            resource = FileManagerResource.NormalizePath(path, resource);
            return Normalize(resource, module);
        }

        private static FileManagerResource Normalize(FileManagerResource resource, string module)
        {
            resource.Text = resource.Text.Replace(".xml", "").ToTitleCaseSentence();
            resource.Icon = resource.IsDirectory ? "folder open outline green icon" : "file code outline green icon";
            resource.Path = resource.IsDirectory ? "" : $"/dashboard/reports/view/Areas/{module}/Reports/{resource.Path}";

            foreach (var item in resource.Children)
            {
                Normalize(item, module);
            }

            return resource;
        }
    }
}