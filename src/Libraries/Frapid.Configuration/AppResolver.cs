using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Frapid.Configuration.Models;
using Newtonsoft.Json;

namespace Frapid.Configuration
{
    public static class AppResolver
    {
        static AppResolver()
        {
            string root = PathMapper.MapPath("~/");
            var files = Directory.GetFiles(root, "AppInfo.json", SearchOption.AllDirectories).ToList();

            var installables = new List<Installable>();

            foreach (string file in files)
            {
                string contents = File.ReadAllText(file, Encoding.UTF8);
                var installable = JsonConvert.DeserializeObject<Installable>(contents);
                installable.DirectoryPath = Path.GetDirectoryName(file);
                installables.Add(installable);
            }

            Installables = installables;
        }

        public static List<Installable> Installables { get; set; }
    }
}