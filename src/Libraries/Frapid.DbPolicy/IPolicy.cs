using System.Threading.Tasks;
using Frapid.DataAccess.Models;

namespace Frapid.DbPolicy
{
    public interface IPolicy
    {
        string ObjectNamespace { get; set; }
        string ObjectName { get; set; }
        AccessTypeEnum AccessType { get; set; }
        bool HasAccess { get; }
        long LoginId { get; set; }
        string Tenant { get; set; }
        Task ValidateAsync();
    }
}