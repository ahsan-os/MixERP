using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.ApplicationState.Cache;
using Frapid.Areas.CSRF;
using Frapid.Calendar.Models;
using Frapid.Calendar.ViewModels;
using Frapid.Dashboard;
using Frapid.DataAccess.Models;

namespace Frapid.Calendar.Controllers.Backend
{
    [AntiForgery]
    public sealed class CategoryController : CalendarBackendController
    {
        [Route("dashboard/calendar/category")]
        [MenuPolicy(OverridePath = "/dashboard/calendar")]
        [HttpPost]
        [AccessPolicy("calendar", "categories", AccessTypeEnum.Create)]
        public async Task<ActionResult> CreateCategoryAsync(CalendarCategory model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }


            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            try
            {
                int categoryId = await CalendarCategoryModel.AddCategoryAsync(this.Tenant, meta, model).ConfigureAwait(true);
                return this.Ok(categoryId);
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [Route("dashboard/calendar/category/order")]
        [MenuPolicy(OverridePath = "/dashboard/calendar")]
        [HttpPut]
        [AccessPolicy("calendar", "categories", AccessTypeEnum.Edit)]
        public async Task<ActionResult> OrderCategoriesAsync(List<CategoryOrder> orderInfo)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }


            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            try
            {
                await CalendarCategoryModel.OrderCategoriesAsync(this.Tenant, meta, orderInfo).ConfigureAwait(true);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [Route("dashboard/calendar/category/{categoryId}")]
        [MenuPolicy(OverridePath = "/dashboard/calendar")]
        [HttpPut]
        [AccessPolicy("calendar", "categories", AccessTypeEnum.Edit)]
        public async Task<ActionResult> UpdateCategoryAsync(int categoryId, CalendarCategory model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }


            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            try
            {
                await CalendarCategoryModel.UpdateCategoryAsync(this.Tenant, meta, categoryId, model).ConfigureAwait(true);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [Route("dashboard/calendar/category/{categoryId}")]
        [MenuPolicy(OverridePath = "/dashboard/calendar")]
        [HttpDelete]
        [AccessPolicy("calendar", "categories", AccessTypeEnum.Delete)]
        public async Task<ActionResult> CreateCategoryAsync(int categoryId)
        {
            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            try
            {
                await CalendarCategoryModel.DeleteCategoryAsync(this.Tenant, meta, categoryId).ConfigureAwait(true);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}