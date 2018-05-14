using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace frapid
{
    internal class AssemblyLoader
    {
        public void PreLoad()
        {
            this.AssembliesFromApplicationBaseDirectory();
        }

        private void AssembliesFromApplicationBaseDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            this.AssembliesFromPath(baseDirectory);

            string privateBinPath = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
            if(Directory.Exists(privateBinPath))
                this.AssembliesFromPath(privateBinPath);
        }

        private void AssembliesFromPath(string path)
        {
            var assemblyFiles = Directory.GetFiles(path).Where
                (
                 file =>
                 {
                     string extension = Path.GetExtension(file);
                     return extension != null && extension.Equals(".dll", StringComparison.OrdinalIgnoreCase);
                 });

            foreach(string assemblyFile in assemblyFiles)
            {
                Assembly.LoadFrom(assemblyFile);
            }
        }
    }
}