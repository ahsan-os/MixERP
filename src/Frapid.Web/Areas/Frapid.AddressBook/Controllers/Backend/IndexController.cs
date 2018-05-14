using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.AddressBook.DAL;
using Frapid.AddressBook.DTO;
using Frapid.AddressBook.QueryModels;
using Frapid.AddressBook.ViewModels;
using Frapid.ApplicationState.Cache;
using Frapid.Areas.CSRF;
using Frapid.Dashboard;
using Frapid.DataAccess.Models;
using Frapid.Framework.Extensions;

namespace Frapid.AddressBook.Controllers.Backend
{
    [AntiForgery]
    public sealed class IndexController : AddressBookBackendController
    {
        [Route("dashboard/address-book")]
        [MenuPolicy]
        [AccessPolicy("addressbook", "contacts", AccessTypeEnum.Read)]
        public async Task<ActionResult> IndexAsync()
        {
            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            string tags = await Tags.GetTagsAsync(this.Tenant, meta.UserId).ConfigureAwait(true);
            var users = await Users.GetUsersAsync(this.Tenant).ConfigureAwait(true);

            var model = new IndexViewModel
            {
                Tags = tags.Or("").Split(','),
                Users = users
            };

            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/Index.cshtml", this.Tenant), model);
        }

        [Route("dashboard/address-book/get")]
        [MenuPolicy(OverridePath = "/dashboard/address-book")]
        [HttpPost]
        [AccessPolicy("addressbook", "contacts", AccessTypeEnum.Read)]
        public async Task<ActionResult> GetAsync(AddressBookQuery query)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            query.UserId = meta.UserId;

            try
            {
                var model = await Contacts.GetContactsAsync(this.Tenant, query).ConfigureAwait(true);
                return this.Ok(model.OrderBy(x => x.FormattedName));
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [Route("dashboard/address-book")]
        [MenuPolicy]
        [HttpPost]
        [AccessPolicy("addressbook", "contacts", AccessTypeEnum.Create)]
        public async Task<ActionResult> CreateContactAsync(Contact model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            try
            {
                var id = await Models.Contacts.CreateContactAsync(this.Tenant, meta, model).ConfigureAwait(true);
                return this.Ok(id);
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [Route("dashboard/address-book/{contactId:guid}")]
        [MenuPolicy(OverridePath = "/dashboard/address-book")]
        [AccessPolicy("addressbook", "contacts", AccessTypeEnum.Read)]
        public async Task<ActionResult> GetContactAsync(Guid contactId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            try
            {
                var model = await Contacts.GetContactAsync(this.Tenant, meta.UserId, contactId).ConfigureAwait(true);
                return this.Ok(model);
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [Route("dashboard/address-book/edit")]
        [MenuPolicy(OverridePath = "/dashboard/address-book")]
        [HttpPut]
        [AccessPolicy("addressbook", "contacts", AccessTypeEnum.Edit)]
        public async Task<ActionResult> EditContactAsync(Contact model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);

            try
            {
                await Models.Contacts.UpdateContactAsync(this.Tenant, meta, model).ConfigureAwait(true);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [Route("dashboard/address-book/delete/{contactId:guid}")]
        [MenuPolicy(OverridePath = "/dashboard/address-book")]
        [HttpDelete]
        [AccessPolicy("addressbook", "contacts", AccessTypeEnum.Delete)]
        public async Task<ActionResult> DeleteContactAsync(Guid contactId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            try
            {
                await Models.Contacts.DeleteContactAsync(this.Tenant, meta, contactId).ConfigureAwait(true);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}