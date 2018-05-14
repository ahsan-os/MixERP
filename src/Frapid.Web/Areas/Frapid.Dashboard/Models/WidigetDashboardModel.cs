using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Frapid.Configuration;
using Frapid.Dashboard.ViewModels;
using Newtonsoft.Json;

namespace Frapid.Dashboard.Models
{
    public static class WidigetDashboardModel
    {
        public const string WidgetLocation = "/Tenants/{tenant}/Areas/Frapid.Dashboard/Widgets";

        public static List<string> GetAreas()
        {
            var areaRoot = new DirectoryInfo(PathMapper.MapPath("/Areas"));
            var directories = areaRoot.GetDirectories().Where(x => x.GetDirectories("widgets", SearchOption.AllDirectories).Any());

            return directories.Select(directory => directory.Name).ToList();
        }

        public static List<string> GetWidgets(string areaName)
        {
            var widgets = new List<string>();
            string areaRoot = PathMapper.MapPath("/Areas");
            string widgetPath = Path.Combine(areaRoot, $"{areaName}/widgets");

            if (!Directory.Exists(widgetPath))
            {
                return widgets;
            }

            var directory = new DirectoryInfo(widgetPath);
            var files = directory.GetFiles("*.html", SearchOption.TopDirectoryOnly);
            widgets.AddRange(files.Select(file => file.Name.Replace(".html", "")));

            return widgets;
        }

        public static string SanitizePath(string filename)
        {
            return string.Join("-", filename.Split(Path.GetInvalidFileNameChars()));
        }

        public static void DeleteMy(string tenant, MyWidgetInfo info)
        {
            string container = PathMapper.MapPath($"{WidgetLocation.Replace("{tenant}", tenant)}/{info.Scope}/{info.Me}");

            string filePath = Path.Combine(container, SanitizePath(info.Name) + ".json");

            if (!File.Exists(filePath))
            {
                return;
            }

            File.Delete(filePath);
        }

        public static MyWidgetInfo GetMy(string tenant, MyWidgetInfo info)
        {
            string container = PathMapper.MapPath($"{WidgetLocation.Replace("{tenant}", tenant)}/{info.Scope}/{info.Me}");
            string filePath = Path.Combine(container, SanitizePath(info.Name) + ".json");

            if (!File.Exists(filePath))
            {
                return info;
            }

            string contents = File.ReadAllText(filePath, new UTF8Encoding(false));
            info = JsonConvert.DeserializeObject<MyWidgetInfo>(contents);

            return info;
        }

        public static List<MyWidgetInfo> GetMy(string tenant, int userId, string scope)
        {
            var widgets = new List<MyWidgetInfo>();

            string container = PathMapper.MapPath($"{WidgetLocation.Replace("{tenant}", tenant)}/{scope}/{userId}");
            if (!Directory.Exists(container))
            {
                return widgets;
            }

            var files = new DirectoryInfo(container).GetFiles("*.json");
            if (!files.Any())
            {
                return widgets;
            }

            foreach (var file in files)
            {
                string contents = File.ReadAllText(file.FullName, new UTF8Encoding(false));
                var info = JsonConvert.DeserializeObject<MyWidgetInfo>(contents);
                widgets.Add(info);
            }

            return widgets;
        }

        public static void SaveMy(string tenant, MyWidgetInfo info)
        {
            string container = PathMapper.MapPath($"{WidgetLocation.Replace("{tenant}", tenant)}/{info.Scope}/{info.Me}");

            if (!Directory.Exists(container))
            {
                Directory.CreateDirectory(container);
            }

            string filePath = Path.Combine(container, SanitizePath(info.Name) + ".json");

            foreach (var widget in info.Widgets)
            {
                widget.Scope = info.Scope;
            }

            string contents = JsonConvert.SerializeObject(info, Formatting.Indented);

            File.WriteAllText(filePath, contents, new UTF8Encoding(false));
        }
    }
}