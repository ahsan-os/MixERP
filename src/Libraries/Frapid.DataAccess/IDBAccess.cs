using System.Threading.Tasks;
using Frapid.DataAccess.Models;

namespace Frapid.DataAccess
{
    public interface IDbAccess
    {
        bool HasAccess { get; }
        Task ValidateAsync(AccessTypeEnum type, long loginId, string database, bool noException);
    }
}