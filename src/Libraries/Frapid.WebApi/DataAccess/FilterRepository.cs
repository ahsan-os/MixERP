using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.DataAccess;
using Frapid.DataAccess.Models;
using Frapid.DbPolicy;
using Frapid.i18n;
using Frapid.Mapper;
using Frapid.Mapper.Query.Delete;
using Frapid.Mapper.Query.Insert;
using Serilog;

namespace Frapid.WebApi.DataAccess
{
    public class FilterRepository : DbAccess
    {
        public FilterRepository(string database, long loginId, int userId)
        {
            this._ObjectNamespace = "config";
            this._ObjectName = "filters";
            this.Database = database;
            this.LoginId = loginId;
            this.UserId = userId;
        }

        public override string _ObjectNamespace { get; }
        public override string _ObjectName { get; }
        public string NameColumn { get; set; }
        public string Database { get; set; }
        public int UserId { get; set; }
        public bool IsValid { get; set; }
        public long LoginId { get; set; }
        public int OfficeId { get; set; }

        public async Task<IEnumerable<Filter>> GetWhereAsync(long pageNumber, List<Filter> filters)
        {
            if (string.IsNullOrWhiteSpace(this.Database))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    await this.ValidateAsync(AccessTypeEnum.Read, this.LoginId, this.Database, false).ConfigureAwait(false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the filtered entity \"Filter\" was denied to the user with Login ID {LoginId}. Filters: {Filters}.", pageNumber, this.LoginId, filters);
                    throw new UnauthorizedException(Resources.AccessIsDenied);
                }
            }

            long offset = (pageNumber - 1)*50;
            var sql = new Sql("SELECT * FROM config.filters WHERE deleted = @0", false);

            FilterManager.AddFilters(ref sql, new Filter(), filters);

            sql.OrderBy("filter_id");

            if (pageNumber > 0)
            {
                sql.Append(FrapidDbServer.AddOffset(this.Database, "@0"), offset);
                sql.Append(FrapidDbServer.AddLimit(this.Database, "@0"), 50);
            }

            try
            {
                return await Factory.GetAsync<Filter>(this.Database, sql).ConfigureAwait(false);
            }
            catch (DbException ex)
            {
                Log.Error(ex.Message);
                
                throw new DataAccessException(this.Database, ex.Message, ex);
            }
        }

        public async Task MakeDefaultAsync(string objectName, string filterName)
        {
            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    await this.ValidateAsync(AccessTypeEnum.CreateFilter, this.LoginId, this.Database, false).ConfigureAwait(false);
                }

                if (!this.HasAccess)
                {
                    Log.Information("Access to create default filter '{FilterName}' for {ObjectName} was denied to the user with Login ID {LoginId}.", filterName, objectName, this.LoginId);
                    throw new UnauthorizedException(Resources.AccessIsDenied);
                }
            }

            const string sql = "UPDATE config.filters SET is_default=true WHERE object_name=@0 AND filter_name=@1;";

            try
            {
                await Factory.NonQueryAsync(this.Database, sql, objectName, filterName).ConfigureAwait(false);
            }
            catch (DbException ex)
            {
                Log.Error(ex.Message);
                throw new DataAccessException(this.Database, ex.Message, ex);
            }
        }

        /// <summary>
        ///     Deletes the row of the table "config.filters" against the supplied filter name.
        /// </summary>
        /// <param name="filterName">The value of the column "filter_name" which will be deleted.</param>
        /// <exception cref="UnauthorizedException">
        ///     Thown when the application user does not have sufficient privilege to perform
        ///     this action.
        /// </exception>
        public async Task DeleteAsync(string filterName)
        {
            if (string.IsNullOrWhiteSpace(this.Database))
            {
                return;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    await this.ValidateAsync(AccessTypeEnum.Delete, this.LoginId, this.Database, false).ConfigureAwait(false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to delete entity \"Filter\" with Filter Name {FilterName} was denied to the user with Login ID {LoginId}.", filterName, this.LoginId);
                    throw new UnauthorizedException(Resources.AccessIsDenied);
                }
            }

            const string sql = "DELETE FROM config.filters WHERE filter_name=@0;";

            try
            {
                await Factory.NonQueryAsync(this.Database, sql, filterName).ConfigureAwait(false);
            }
            catch (DbException ex)
            {
                Log.Error(ex.Message);
                throw new DataAccessException(this.Database, ex.Message, ex);
            }
        }

        public async Task RecreateFiltersAsync(string objectName, string filterName, List<Filter> filters)
        {
            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    await this.ValidateAsync(AccessTypeEnum.Create, this.LoginId, this.Database, false).ConfigureAwait(false);
                }

                if (!this.HasAccess)
                {
                    Log.Information("Access to add entity \"Filter\" was denied to the user with Login ID {LoginId}. {filters}", this.LoginId, filters);
                    throw new UnauthorizedException(Resources.AccessIsDenied);
                }
            }


            using (var db = DbProvider.GetDatabase(this.Database))
            {
                try
                {
                    await db.BeginTransactionAsync().ConfigureAwait(false);

                    var toDelete = await this.GetWhereAsync
                        (
                            1,
                            new List<Filter>
                            {
                                new Filter
                                {
                                    ColumnName = "object_name",
                                    FilterCondition = (int) FilterCondition.IsEqualTo,
                                    FilterValue = objectName
                                },
                                new Filter
                                {
                                    ColumnName = "filter_name",
                                    FilterCondition = (int) FilterCondition.IsEqualTo,
                                    FilterValue = filterName
                                }
                            }).ConfigureAwait(false);


                    foreach (long filterId in toDelete.Select(x => x.FilterId))
                    {
                        await db.DeleteAsync(filterId, "config.filters", "filter_id").ConfigureAwait(false);
                    }

                    foreach (var filter in filters)
                    {
                        filter.AuditUserId = this.UserId;
                        filter.AuditTs = DateTimeOffset.UtcNow;

                        await db.InsertAsync("config.filters", "filter_id", true, filter).ConfigureAwait(false);
                    }

                    db.CommitTransaction();
                }
                catch(Exception ex)
                {
                    db.RollbackTransaction();
                    Log.Error(ex.Message);
                    throw new DataAccessException(this.Database, ex.Message, ex);
                }
            }
        }

        public async Task RemoveDefaultAsync(string objectName)
        {
            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    await this.ValidateAsync(AccessTypeEnum.CreateFilter, this.LoginId, this.Database, false).ConfigureAwait(false);
                }

                if (!this.HasAccess)
                {
                    Log.Information("Access to delete default filter for {ObjectName} was denied to the user with Login ID {LoginId}.", objectName, this.LoginId);
                    throw new UnauthorizedException(Resources.AccessIsDenied);
                }
            }

            const string sql = "UPDATE config.filters SET is_default=false WHERE object_name=@0;";

            try
            {
                await Factory.NonQueryAsync(this.Database, sql, objectName).ConfigureAwait(false);
            }
            catch (DbException ex)
            {
                Log.Error(ex.Message);
                throw new DataAccessException(this.Database, ex.Message, ex);
            }
        }
    }
}