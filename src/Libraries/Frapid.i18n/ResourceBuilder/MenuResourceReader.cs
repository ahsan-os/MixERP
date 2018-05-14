using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Configuration.Models;
using Frapid.Mapper;
using Frapid.Mapper.Query.Select;

namespace Frapid.i18n.ResourceBuilder
{
    public sealed class MenuResourceReader : IResourceReader
    {
        public async Task<Dictionary<string, string>> GetResourcesAsync(string tenant, Installable app, string path)
        {
            var resources = new Dictionary<string, string>();

            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT i18n_key, menu_name FROM core.menus");
                sql.Where("app_name=@0", app.ApplicationName);

                var columns = await db.SelectAsync<dynamic>(sql).ConfigureAwait(false);

                foreach (var column in columns)
                {
                    string key = column.I18nKey;
                    string value = column.MenuName;

                    resources.Add(key, value);
                }
            }

            return resources;
        }
    }
}