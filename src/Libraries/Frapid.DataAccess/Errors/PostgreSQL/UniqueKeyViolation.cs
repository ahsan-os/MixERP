using System;
using System.ComponentModel;
using System.Data.Common;
using Frapid.Mapper.Database;
using Npgsql;

namespace Frapid.DataAccess.Errors.PostgreSQL
{
    public sealed class UniqueKeyViolation : IDbErrorMessage
    {
        [Localizable(true)]
        public string ErrorMessage { get; } = "Duplicate entry. Please recheck the form and try again.";

        public DatabaseType DatabaseType { get; } = DatabaseType.PostgreSQL;
        public string[] Identifiers { get; } = {"23505"};

        public string Parse(DbException ex)
        {
            var inner = ex as PostgresException;

            if (inner == null)
            {
                return ex.Message;
            }

            return this.ErrorMessage + Environment.NewLine + inner.Detail;
        }
    }
}