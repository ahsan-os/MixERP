using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Configuration.Models;
using Frapid.Mapper;
using Frapid.Mapper.Extensions;
using Frapid.Mapper.Query.Select;

namespace Frapid.i18n.ResourceBuilder
{
    public sealed class ColumnResourceReader : IResourceReader
    {
        public async Task<Dictionary<string, string>> GetResourcesAsync(string tenant, Installable app, string path)
        {
            var resources = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(app.DbSchema))
            {
                return resources;
            }

            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT DISTINCT column_name FROM information_schema.columns");
                sql.Where("table_schema=@0", app.DbSchema);

                var columns = await db.SelectAsync<dynamic>(sql).ConfigureAwait(false);

                foreach (var column in columns)
                {
                    string name = column.ColumnName;

                    string key = name.ToPascalCase();
                    string value = name.ToSentence().ToTitleCaseSentence();

                    if (!resources.ContainsKey(key))
                    {
                        resources.Add(key, value);
                    }
                }
            }
            return resources;
        }
    }
}