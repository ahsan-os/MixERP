using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Frapid.Configuration;
using Frapid.Configuration.Models;
using Newtonsoft.Json;

namespace Frapid.Installer
{
    public static class AppResolver
    {
        public static List<Installable> Installables { get; set; }

        static AppResolver()
        {
            string root = PathMapper.MapPath("~/");
            var files = Directory.GetFiles(root, "AppInfo.json", SearchOption.AllDirectories).ToList();
            Installables = files.Select(file => File.ReadAllText(file, Encoding.UTF8)).Select(JsonConvert.DeserializeObject<Installable>).ToList();            
        }
    }
}