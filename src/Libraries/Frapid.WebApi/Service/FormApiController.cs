using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Frapid.DataAccess;
using Frapid.DataAccess.Models;
using Frapid.Framework;
using Frapid.Mapper.Helpers;
using Frapid.WebApi.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Frapid.WebApi.Service
{
    public class FormApiController : FrapidApiController
    {
        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/meta")]
        [RestAuthorize]
        public async Task<EntityView> GetEntityViewAsync(string schemaName, string tableName)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetMetaAsync().ConfigureAwait(true);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/count")]
        [RestAuthorize]
        public async Task<long> CountAsync(string schemaName, string tableName)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.CountAsync().ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/all")]
        [Route("~/api/forms/{schemaName}/{tableName}/export")]
        [RestAuthorize]
        public async Task<IEnumerable<dynamic>> GetAllAsync(string schemaName, string tableName)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetAllAsync().ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/{primaryKey}")]
        [RestAuthorize]
        public async Task<dynamic> GetAsync(string schemaName, string tableName, string primaryKey)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetAsync(primaryKey).ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/get")]
        [RestAuthorize]
        public async Task<IEnumerable<dynamic>> GetAsync(string schemaName, string tableName, [FromUri] object[] primaryKeys)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetAsync(primaryKeys).ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/first")]
        [RestAuthorize]
        public async Task<dynamic> GetFirstAsync(string schemaName, string tableName)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetFirstAsync().ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/previous/{primaryKey}")]
        [RestAuthorize]
        public async Task<dynamic> GetPreviousAsync(string schemaName, string tableName, string primaryKey)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetPreviousAsync(primaryKey).ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/next/{primaryKey}")]
        [RestAuthorize]
        public async Task<dynamic> GetNextAsync(string schemaName, string tableName, string primaryKey)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetNextAsync(primaryKey).ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/last")]
        [RestAuthorize]
        public async Task<dynamic> GetLastAsync(string schemaName, string tableName)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetLastAsync().ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}")]
        [RestAuthorize]
        public async Task<IEnumerable<dynamic>> GetPaginatedResultAsync(string schemaName, string tableName)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetPaginatedResultAsync().ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/page/{pageNumber}")]
        [RestAuthorize]
        public async Task<IEnumerable<dynamic>> GetPaginatedResultAsync(string schemaName, string tableName, long pageNumber)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                var result = await repository.GetPaginatedResultAsync(pageNumber).ConfigureAwait(false);
                return result;
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

        [AcceptVerbs("POST")]
        [Route("~/api/forms/{schemaName}/{tableName}/count-where")]
        [RestAuthorize]
        public async Task<long> CountWhereAsync(string schemaName, string tableName, [FromBody] List<Filter> filters)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.CountWhereAsync(filters).ConfigureAwait(false);
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


        [AcceptVerbs("POST")]
        [Route("~/api/forms/{schemaName}/{tableName}/get-where/{pageNumber}")]
        [RestAuthorize]
        public async Task<IEnumerable<dynamic>> GetWhereAsync(string schemaName, string tableName, long pageNumber, [FromBody] List<Filter> filters)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetWhereAsync(pageNumber, filters).ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/count-filtered/{filterName}")]
        [RestAuthorize]
        public async Task<long> CountFilteredAsync(string schemaName, string tableName, string filterName)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.CountFilteredAsync(filterName).ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/get-filtered/{pageNumber}/{filterName}")]
        [RestAuthorize]
        public async Task<IEnumerable<dynamic>> GetFilteredAsync(string schemaName, string tableName, long pageNumber, string filterName)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetFilteredAsync(pageNumber, filterName).ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/display-fields")]
        [RestAuthorize]
        public async Task<IEnumerable<DisplayField>> GetDisplayFieldsAsync(string schemaName, string tableName)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetDisplayFieldsAsync().ConfigureAwait(false);
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

        [AcceptVerbs("POST")]
        [Route("~/api/forms/{schemaName}/{tableName}/display-fields/get-where")]
        [HttpPost]
        [RestAuthorize]
        public async Task<IEnumerable<DisplayField>> GetDisplayFieldsAsync(string schemaName, string tableName, [FromBody] List<Filter> filters)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetDisplayFieldsAsync(filters).ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/lookup-fields")]
        [RestAuthorize]
        public async Task<IEnumerable<DisplayField>> GetLookupFieldsAsync(string schemaName, string tableName)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetLookupFieldsAsync().ConfigureAwait(false);
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

        [AcceptVerbs("POST")]
        [Route("~/api/forms/{schemaName}/{tableName}/lookup-fields/get-where")]
        [HttpPost]
        [RestAuthorize]
        public async Task<IEnumerable<DisplayField>> GetLookupFieldsAsync(string schemaName, string tableName, [FromBody] List<Filter> filters)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetLookupFieldsAsync(filters).ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/custom-fields")]
        [RestAuthorize]
        public async Task<IEnumerable<CustomField>> GetCustomFieldsAsync(string schemaName, string tableName)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetCustomFieldsAsync(null).ConfigureAwait(false);
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

        [AcceptVerbs("GET", "HEAD")]
        [Route("~/api/forms/{schemaName}/{tableName}/custom-fields/{resourceId}")]
        [RestAuthorize]
        public async Task<IEnumerable<CustomField>> GetCustomFieldsAsync(string schemaName, string tableName, string resourceId)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.GetCustomFieldsAsync(resourceId).ConfigureAwait(false);
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

        [AcceptVerbs("POST")]
        [Route("~/api/forms/{schemaName}/{tableName}/add-or-edit")]
        [RestAuthorize]
        public async Task<object> AddOrEditAsync(string schemaName, string tableName, [FromBody] JArray form)
        {
            var meta = form[0].ToObject<EntityView>();
            var item = this.GetPostedData(meta, form[1].ToObject<Dictionary<string, object>>());
            var customFields = form[2].ToObject<List<CustomField>>(JsonHelper.GetJsonSerializer());

            if (item == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.AddOrEditAsync(item, customFields).ConfigureAwait(false);
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

        public Dictionary<string, object> GetPostedData(EntityView meta, Dictionary<string, object> items)
        {
            if (meta == null || !meta.Columns.Any())
            {
                return items;
            }

            var parsed = new Dictionary<string, object>();

            foreach (var item in items)
            {
                var column = meta.Columns.FirstOrDefault(x => x.PropertyName == item.Key);
                var type = column?.DataType.GetWellKnownType();
                var converted = TypeConverter.Convert(item.Value, type);


                parsed.Add(item.Key, converted ?? item.Value);
            }

            return parsed;
        }

        [AcceptVerbs("POST")]
        [Route("~/api/forms/{schemaName}/{tableName}/add")]
        [Route("~/api/forms/{schemaName}/{tableName}/add/{skipPrimaryKey:bool}")]
        [RestAuthorize]
        public async Task<object> AddAsync(string schemaName, string tableName, [FromBody] JArray form, bool skipPrimaryKey = true)
        {
            var meta = form[0].ToObject<EntityView>();
            var item = this.GetPostedData(meta, form[1].ToObject<Dictionary<string, object>>());
            var customFields = form[2].ToObject<List<CustomField>>(JsonHelper.GetJsonSerializer());

            if (item == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.AddAsync(item, customFields, skipPrimaryKey).ConfigureAwait(false);
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
        [Route("~/api/forms/{schemaName}/{tableName}/edit/{primaryKey}")]
        [RestAuthorize]
        public async Task<object> EditAsync(string schemaName, string tableName, string primaryKey, [FromBody] JArray form)
        {
            var meta = form[0].ToObject<EntityView>();
            var item = this.GetPostedData(meta, form[1].ToObject<Dictionary<string, object>>());
            var customFields = form[2].ToObject<List<CustomField>>(JsonHelper.GetJsonSerializer());

            if (item == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                await repository.UpdateAsync(item, primaryKey, customFields, meta).ConfigureAwait(false);
                return primaryKey;
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

        private List<Dictionary<string, object>> ParseCollection(EntityView meta, JArray collection)
        {
            var items = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(collection.ToString(), JsonHelper.GetJsonSerializerSettings());

            for (int i = 0; i < items.Count; i++)
            {
                var item = this.GetPostedData(meta, items[i]);
                items[i] = item;
            }

            return items;
        }

        [AcceptVerbs("POST")]
        [Route("~/api/forms/{schemaName}/{tableName}/bulk-import")]
        [RestAuthorize]
        public async Task<List<object>> BulkImportAsync(string schemaName, string tableName, [FromBody] JArray collection)
        {
            var meta = collection[0].ToObject<EntityView>();
            var items = this.ParseCollection(meta, collection[1].ToObject<JArray>());

            if (items == null || items.Count.Equals(0))
            {
                return null;
            }

            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                return await repository.BulkImportAsync(items).ConfigureAwait(false);
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
        [Route("~/api/forms/{schemaName}/{tableName}/delete/{primaryKey}")]
        [RestAuthorize]
        public async Task DeleteAsync(string schemaName, string tableName, string primaryKey)
        {
            try
            {
                var repository = new FormRepository(schemaName, tableName, this.AppUser.Tenant, this.AppUser.LoginId, this.AppUser.UserId);
                await repository.DeleteAsync(primaryKey).ConfigureAwait(false);
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