using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using Frapid.Mapper.Database;

namespace Frapid.DataAccess.Errors.SqlServer
{
    public sealed class UniqueKeyViolation : IDbErrorMessage
    {
        [Localizable(true)]
        public string ErrorMessage { get; } = "Duplicate entry. Please recheck the form and try again.";

        public DatabaseType DatabaseType { get; } = DatabaseType.SqlServer;
        public string[] Identifiers { get; } = {"2601", "2627"};

        public string Parse(DbException ex)
        {
            var inner = ex as SqlException;

            if (ex == null)
            {
                return ex.Message;
            }


            return this.ErrorMessage + Environment.NewLine + inner.Message;
        }
    }
}