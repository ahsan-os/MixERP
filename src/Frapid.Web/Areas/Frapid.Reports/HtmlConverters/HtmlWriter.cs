using System.IO;
using System.Text;
using Frapid.Configuration;

namespace Frapid.Reports.HtmlConverters
{
    public static class HtmlWriter
    {
        public static void WriteHtml(string path, string html)
        {
            var destination = new FileInfo(PathMapper.MapPath(path));

            if (destination.Directory != null && !destination.Directory.Exists)
            {
                destination.Directory.Create();
            }

            File.WriteAllText(destination.FullName, html, new UTF8Encoding(false));
        }
    }
}