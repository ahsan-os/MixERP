using System.IO;
using System.Reflection;

namespace Frapid.Configuration
{
    public static class ConsolePathMapper
    {
        public static void SetPathToRoot()
        {
            string root = string.Empty;
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (path != null)
            {
                var directory = new DirectoryInfo(path);

                if (directory.Parent != null)
                {
                    root = directory.Parent.FullName;
                }
            }

            if (string.IsNullOrWhiteSpace(root))
            {
                throw new DirectoryNotFoundException("Cannot determine application root directory.");
            }

            PathMapper.PathToRootDirectory = root;
        }
    }
}