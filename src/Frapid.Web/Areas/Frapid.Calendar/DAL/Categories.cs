using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Calendar.DTO;
using Frapid.Calendar.ViewModels;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Framework.Extensions;
using Frapid.Mapper.Query.Insert;
using Frapid.Mapper.Query.NonQuery;
using Frapid.Mapper.Query.Select;
using Frapid.Mapper.Query.Update;

namespace Frapid.Calendar.DAL
{
    public static class Categories
    {
        public static async Task<Category> GetCategoryAsync(string tenant, int categoryId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                const string sql = "SELECT * FROM calendar.categories WHERE category_id = @0 AND deleted = @1;";
                var awaiter = await db.SelectAsync<Category>(sql, categoryId, false).ConfigureAwait(false);

                return awaiter.FirstOrDefault();
            }
        }

        public static async Task<IEnumerable<Category>> GetMyCategoriesAsync(string tenant, int userId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                const string sql = "SELECT * FROM calendar.categories WHERE user_id = @0 AND deleted = @1;";
                return await db.SelectAsync<Category>(sql, userId, false).ConfigureAwait(false);
            }
        }

        public static async Task<short> CountCategoriesAsync(string tenant, int userId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                const string sql = "SELECT COUNT(*) FROM calendar.categories WHERE user_id = @0 AND deleted = @1;";
                return await db.ScalarAsync<short>(sql, userId, false).ConfigureAwait(false);
            }
        }

        public static async Task<long> CountEventsAsync(string tenant, int categoryId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                const string sql = "SELECT COUNT(*) FROM calendar.events WHERE category_id = @0 AND deleted = @1;";
                return await db.ScalarAsync<long>(sql, categoryId, false).ConfigureAwait(false);
            }
        }

        public static async Task<int> AddCategoryAsync(string tenant, Category category)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                short count = await CountCategoriesAsync(tenant, category.UserId).ConfigureAwait(false);
                count++;

                category.CategoryOrder = count;
                var id = await db.InsertAsync(category).ConfigureAwait(false);

                return id.To<int>();
            }
        }

        public static async Task UpdateCategoryAsync(string tenant, int categoryId, Category category)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                await db.UpdateAsync(category, categoryId).ConfigureAwait(false);
            }
        }

        public static async Task DeleteCategoryAsync(string tenant, int categoryId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                const string sql = "UPDATE calendar.categories SET deleted = @0 WHERE category_id = @1";

                await db.NonQueryAsync(sql, true, categoryId).ConfigureAwait(false);
            }
        }

        public static async Task SaveOrderAsync(string tenant, List<CategoryOrder> orderInfo)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                await db.BeginTransactionAsync().ConfigureAwait(false);

                try
                {
                    foreach (var info in orderInfo)
                    {
                        const string sql = "UPDATE calendar.categories SET category_order = @0 WHERE category_id = @1";
                        await db.NonQueryAsync(sql, info.Order, info.CategoryId).ConfigureAwait(false);
                    }

                    db.CommitTransaction();
                }
                catch
                {
                    db.RollbackTransaction();
                    throw;
                }
            }
        }
    }
}