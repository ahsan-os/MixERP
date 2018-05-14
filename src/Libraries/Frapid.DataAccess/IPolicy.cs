using Frapid.DataAccess.Models;

namespace Frapid.DataAccess
{
    public interface IPolicy
    {
        string ObjectNamespace { get; set; }
        string ObjectName { get; set; }
        AccessTypeEnum AccessType { get; set; }
        bool HasAccess { get; }
        long LoginId { get; set; }
        string Database { get; set; }
        void Validate();
    }
}