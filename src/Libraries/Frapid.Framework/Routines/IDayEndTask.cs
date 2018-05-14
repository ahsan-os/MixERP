using System.Threading.Tasks;

namespace Frapid.Framework.Routines
{
    public interface IDayEndTask
    {
        string[] Tenants { get; set; }
        string Description { get; set; }
        Task RegisterAsync();
    }
}