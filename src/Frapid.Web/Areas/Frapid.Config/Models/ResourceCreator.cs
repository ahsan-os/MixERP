using System.IO;
using System.Text;
using System.Web.Hosting;

namespace Frapid.Config.Models
{
    public sealed class ResourceCreator
    {
        public string Container { get; set; }
        public string File { get; set; }
        public bool IsDirectory { get; set; }
        public string Contents { get; set; }
        public bool Rewrite { get; set; }

        public void Create(string tenant)
        {
            string path = $"~/Tenants/{tenant}";
            path = HostingEnvironment.MapPath(path);

            if (path == null || !Directory.Exists(path))
            {
                throw new ResourceCreateException(I18N.CouldNotCreateFileTenantDirectoryMissing);
            }

            path = Path.Combine(path, this.Container);

            if (!Directory.Exists(path))
            {
                throw new ResourceCreateException(I18N.CouldNotCreateFileOrDirectoryInvalidPath);
            }

            path = Path.Combine(path, this.File);

            if (this.Rewrite.Equals(false) && System.IO.File.Exists(path))
            {
                return;
            }

            if (this.IsDirectory)
            {
                Directory.CreateDirectory(path);
                return;
            }

            System.IO.File.WriteAllText(path, this.Contents, new UTF8Encoding(false));
        }
    }
}