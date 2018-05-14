using System.Data.Common;
using Frapid.Mapper.Database;

namespace Frapid.DataAccess
{
    interface IDbErrorMessage
    {
        DatabaseType DatabaseType { get; }
        string[] Identifiers { get; }

        string Parse(DbException ex);
    }
}