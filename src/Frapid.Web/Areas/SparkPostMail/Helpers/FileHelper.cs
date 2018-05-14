using System.Linq;
using System.IO;

namespace SparkPostMail.Helpers
{
    internal static class FileHelper
    {
        internal static void DeleteFiles(params string[] files)
        {
            foreach (string file in files.Where(file => !string.IsNullOrWhiteSpace(file)).Where(File.Exists))
            {
                File.Delete(file);
            }
        }
    }
}