using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Frapid.DataAccess;
using Frapid.DataAccess.Models;
using Frapid.DbPolicy;
using Frapid.i18n;
using Frapid.Mapper;
using Serilog;

namespace Frapid.WebApi.DataAccess
{
    public class KanbanRepository : DbAccess
    {
        public KanbanRepository(string database, long loginId, int userId)
        {
            this._ObjectNamespace = "config";
            this._ObjectName = "kanban_details";
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

        public async Task<IEnumerable<dynamic>> GetAsync(long[] kanbanIds, object[] resourceIds)
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
                    Log.Information("Access to entity \"KanbanDetail\" was denied to the user with Login ID {LoginId}. KanbanId: {KanbanIds}, ResourceIds {ResourceIds}.", this.LoginId, kanbanIds,
                        resourceIds);
                    throw new UnauthorizedException(Resources.AccessIsDenied);
                }
            }


            if (kanbanIds == null ||
                resourceIds == null ||
                !kanbanIds.Any() ||
                !resourceIds.Any())
            {
                return new List<dynamic>();
            }


            var sql = new Sql("SELECT * FROM config.kanban_details WHERE deleted=@0 AND", false);
            sql.In("kanban_id IN(@0)", kanbanIds);
            sql.Append("AND");
            sql.In("resource_id IN(@0)", resourceIds);

            try
            {
                return await Factory.GetAsync<dynamic>(this.Database, sql).ConfigureAwait(false);
            }
            catch (DbException ex)
            {
                Log.Error(ex.Message);
                throw new DataAccessException(this.Database, ex.Message, ex);
            }
        }
    }
}