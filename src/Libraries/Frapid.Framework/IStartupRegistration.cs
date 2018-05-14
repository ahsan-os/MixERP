using System.Threading.Tasks;

namespace Frapid.Framework
{
    public interface IStartupRegistration
    {
        string Description { get; set; }
        Task RegisterAsync();
    }
}