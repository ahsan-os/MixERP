using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Frapid.DataAccess;
using Frapid.WebApi.DataAccess;

namespace Frapid.WebApi.Service
{
    public class KanbanApiController : FrapidApiController
    {
        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/kanbans/get-by-resources")]
        [RestAuthorize]
        public async Task<IEnumerable<dynamic>> GetAsync([FromUri] long[] kanbanIds, [FromUri] object[] resourceIds)
        {
            if (kanbanIds == null || resourceIds == null)
            {
                return null;
            }

            try
            {
                var repository = new KanbanRepository(this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetAsync(kanbanIds, resourceIds).ConfigureAwait(false);
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