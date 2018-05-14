using System.Linq;
using System.Threading.Tasks;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Select;
using Frapid.Messaging.DTO;

namespace Frapid.Messaging.DAL
{
    public class Smtp
    {
        public static async Task<SmtpConfig> GetConfigAsync(string tenant)
        {
            using (var db = DbProvider.GetDatabase(tenant))
            {
                var sql = new Sql("SELECT * FROM config.smtp_configs");
                sql.Where("enabled=@0", true);
                sql.And("deleted=@0",false);
                sql.And("is_default=@0", true);
                sql.Limit(db.DatabaseType, 1, 0, "smtp_config_id");

                var awaiter = await db.SelectAsync<SmtpConfig>(sql).ConfigureAwait(false);

                return awaiter.FirstOrDefault();
            }
        }
    }
}