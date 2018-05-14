using System.IO;
using System.Web.Hosting;

namespace Frapid.Config.Models
{
    public sealed class ResourceRemover
    {
        public ResourceRemover(string resource)
        {
            this.Resource = resource;
        }

        public string Resource { get; }

        public void Delete(string tenant)
        {
            string path = $"~/Tenants/{tenant}";
            path = HostingEnvironment.MapPath(path);

            if (path == null)
            {
                throw new ResourceRemoveException(I18N.InvalidPathToFileOrDirectory);
            }

            path = Path.Combine(path, this.Resource);

            if (Directory.Exists(path))
            {
                Directory.Delete(path);
                return;
            }

            if (File.Exists(path))
            {
                File.Delete(path);
                return;
            }

            throw new ResourceRemoveException(I18N.FileOrDirectoryNotFound);
        }
    }
}