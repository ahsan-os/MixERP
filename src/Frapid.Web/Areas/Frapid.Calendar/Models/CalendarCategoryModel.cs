using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.ApplicationState.Models;
using Frapid.Calendar.DAL;
using Frapid.Calendar.DTO;
using Frapid.Calendar.ViewModels;

namespace Frapid.Calendar.Models
{
    public static class CalendarCategoryModel
    {
        public static async Task<int> AddCategoryAsync(string tenant, LoginView meta, CalendarCategory model)
        {
            var poco = new Category
            {
                CategoryName = model.CategoryName,
                ColorCode = model.ColorCode,
                AuditUserId = meta.UserId,
                UserId = meta.UserId,
                AuditTs = DateTimeOffset.UtcNow,
                IsLocal = true,
                Deleted = false
            };

            int categoryId = await Categories.AddCategoryAsync(tenant, poco).ConfigureAwait(false);
            return categoryId;
        }

        private static async Task<bool> BelongsToAsync(string tenant, int userId, int categoryId)
        {
            var category = await Categories.GetCategoryAsync(tenant, categoryId).ConfigureAwait(false);
            return category?.UserId == userId;
        }

        public static async Task UpdateCategoryAsync(string tenant, LoginView meta, int categoryId, CalendarCategory model)
        {
            int userId = meta.UserId;
            bool isMy = await BelongsToAsync(tenant, userId, categoryId).ConfigureAwait(false);

            if (!isMy)
            {
                throw new CalendarCategoryEditException(I18N.AccessIsDenied);
            }

            var poco = new Category
            {
                CategoryId = categoryId,
                CategoryName = model.CategoryName,
                ColorCode = model.ColorCode,
                AuditUserId = meta.UserId,
                UserId = meta.UserId,
                AuditTs = DateTimeOffset.UtcNow,
                IsLocal = true,
                Deleted = false
            };

            await Categories.UpdateCategoryAsync(tenant, categoryId, poco).ConfigureAwait(false);
        }

        public static async Task DeleteCategoryAsync(string tenant, LoginView meta, int categoryId)
        {
            int userId = meta.UserId;
            bool isMy = await BelongsToAsync(tenant, userId, categoryId).ConfigureAwait(false);

            if (!isMy)
            {
                throw new CalendarCategoryEditException(I18N.AccessIsDenied);
            }

            long totalEvents = await Categories.CountEventsAsync(tenant, categoryId).ConfigureAwait(false);

            if (totalEvents > 0)
            {
                throw new CalendarCategoryEditException(I18N.CannotDeleteBecauseEventsUnderCategory);
            }

            await Categories.DeleteCategoryAsync(tenant, categoryId).ConfigureAwait(false);
        }

        public static async Task OrderCategoriesAsync(string tenant, LoginView meta, List<CategoryOrder> orderInfo)
        {
            int userId = meta.UserId;

            foreach (var info in orderInfo)
            {
                bool isMy = await BelongsToAsync(tenant, userId, info.CategoryId).ConfigureAwait(false);

                if (!isMy)
                {
                    throw new CalendarCategoryEditException(I18N.AccessIsDenied);
                }
            }

            await Categories.SaveOrderAsync(tenant, orderInfo).ConfigureAwait(false);
        }
    }
}