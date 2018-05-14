using System.Threading.Tasks;

namespace Frapid.Installer.DAL
{
    public interface IStore
    {
        string ProviderName { get; }
        Task CreateDbAsync(string tenant);
        Task<bool> HasDbAsync(string tenant, string database);
        Task<bool> HasSchemaAsync(string tenant, string database, string schema);
        Task RunSqlAsync(string tenant, string database, string fromFile);
        Task CleanupDbAsync(string tenant, string database);
    }
}