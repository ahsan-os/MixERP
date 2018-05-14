using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Frapid.DataAccess;
using Frapid.DataAccess.Models;
using Frapid.WebApi.DataAccess;

namespace Frapid.WebApi.Service
{
    public class FilterApiController : FrapidApiController
    {
        [AcceptVerbs("PUT")]
        [Route("~/api/filters/make-default/{objectName}/{filterName}")]
        [RestAuthorize]
        public async Task MakeDefaultAsync(string objectName, string filterName)
        {
            try
            {
                var repository = new FilterRepository(this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                await repository.MakeDefaultAsync(objectName, filterName).ConfigureAwait(false);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch (DataAccessException ex)
            {
                throw new HttpResponseException
                    (
                    new HttpResponseMessage
                    {
                        Content = new StringContent(ex.Message),
                        StatusCode = HttpStatusCode.InternalServerError
                    });
            }
#if !DEBUG
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
#endif
        }

        [AcceptVerbs("DELETE")]
        [Route("~/api/filters/remove-default/{objectName}")]
        [RestAuthorize]
        public async Task RemoveDefaultAsync(string objectName)
        {
            try
            {
                var repository = new FilterRepository(this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                await repository.RemoveDefaultAsync(objectName).ConfigureAwait(false);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch (DataAccessException ex)
            {
                throw new HttpResponseException
                    (
                    new HttpResponseMessage
                    {
                        Content = new StringContent(ex.Message),
                        StatusCode = HttpStatusCode.InternalServerError
                    });
            }
#if !DEBUG
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
#endif
        }

        [AcceptVerbs("DELETE")]
        [Route("~/api/filters/delete/by-name/{filterName}")]
        [RestAuthorize]
        public async Task DeleteAsync(string filterName)
        {
            try
            {
                var repository = new FilterRepository(this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                await repository.DeleteAsync(filterName).ConfigureAwait(false);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch (DataAccessException ex)
            {
                throw new HttpResponseException
                    (
                    new HttpResponseMessage
                    {
                        Content = new StringContent(ex.Message),
                        StatusCode = HttpStatusCode.InternalServerError
                    });
            }
#if !DEBUG
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
#endif
        }

        [AcceptVerbs("PUT")]
        [Route("~/api/filters/recreate/{objectName}/{filterName}")]
        [RestAuthorize]
        public async Task RecreateFiltersAsync(string objectName, string filterName, [FromBody] List<Filter> collection)
        {
            try
            {
                var repository = new FilterRepository(this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                await repository.RecreateFiltersAsync(objectName, filterName, collection).ConfigureAwait(false);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch (DataAccessException ex)
            {
                throw new HttpResponseException
                    (
                    new HttpResponseMessage
                    {
                        Content = new StringContent(ex.Message),
                        StatusCode = HttpStatusCode.InternalServerError
                    });
            }
#if !DEBUG
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
#endif
        }
    }
}