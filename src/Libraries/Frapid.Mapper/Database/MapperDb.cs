using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Frapid.Mapper.Types;

namespace Frapid.Mapper.Database
{
    public sealed class MapperDb : IDisposable
    {
        public MapperDb(DatabaseType databaseType, DbProviderFactory dbFactory, string connectionString)
        {
            this.DatabaseType = databaseType;
            this.DbFactory = dbFactory;
            this.ConnectionString = connectionString;
            this.InitializeConnection();
        }

        public bool CacheResults { get; set; } = true;
        public int CacheMilliseconds { get; set; } = 5000; //5 seconds
        public int CommandTimeout { get; set; } = 600;//10 seconds
        public DatabaseType DatabaseType { get; set; }
        public DbProviderFactory DbFactory { get; set; }
        public string ConnectionString { get; set; }
        private DbConnection Connection { get; set; }
        private DbTransaction Transaction { get; set; }


        public void Dispose()
        {
            this.Transaction?.Dispose();
            this.Transaction = null;
            this.Connection?.Dispose();
            this.Connection = null;
        }

        public async Task OpenSharedConnectionAsync()
        {
            if (this.Connection.State == ConnectionState.Broken)
            {
                this.Connection.Close();
            }

            if (this.Connection.State == ConnectionState.Closed)
            {
                await this.Connection.OpenAsync().ConfigureAwait(false);
            }
        }

        public async Task BeginTransactionAsync()
        {
            await this.OpenSharedConnectionAsync().ConfigureAwait(false);
            this.Transaction = this.Connection.BeginTransaction();
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            this.Transaction = this.Connection.BeginTransaction(isolationLevel);
        }

        public DbTransaction GetTransaction()
        {
            return this.Transaction;
        }

        public void CommitTransaction()
        {
            if (this.Transaction == null)
            {
                throw new MapperException("No transaction to commit.");
            }

            this.Transaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (this.Transaction == null)
            {
                throw new MapperException("No transaction to rollback.");
            }

            this.Transaction.Rollback();
        }

        public DbConnection GetConnection()
        {
            return this.Connection;
        }

        private void InitializeConnection()
        {
            if (this.Connection != null)
            {
                return;
            }

            this.Connection = this.DbFactory.CreateConnection();

            if (this.Connection == null)
            {
                throw new MapperException("Could not create connection.");
            }

            this.Connection.ConnectionString = this.ConnectionString;
        }
    }
}