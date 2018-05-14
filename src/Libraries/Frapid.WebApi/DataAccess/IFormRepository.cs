using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.DataAccess.Models;

namespace Frapid.WebApi.DataAccess
{
    public interface IFormRepository
    {
        long LoginId { get; set; }
        int OfficeId { get; set; }

        /// <summary>
        ///     Counts the number of rows in IFormRepository.
        /// </summary>
        /// <returns>Returns the count of IFormRepository.</returns>
        Task<long> CountAsync();

        /// <summary>
        ///     Returns all instances of IFormRepository.
        /// </summary>
        /// <returns>Returns a non-live, non-mapped instances of IFormRepository.</returns>
        Task<IEnumerable<dynamic>> GetAllAsync();

        /// <summary>
        ///     Returns a single instance of the IFormRepository against primary key.
        /// </summary>
        /// <param name="primaryKey">The primary key parameter used on where filter.</param>
        /// <returns>Returns a non-live, non-mapped instance of IFormRepository.</returns>
        Task<dynamic> GetAsync(object primaryKey);

        /// <summary>
        ///     Gets the first record of IFormRepository.
        /// </summary>
        /// <returns>Returns a non-live, non-mapped instance of IFormRepository.</returns>
        Task<dynamic> GetFirstAsync();

        /// <summary>
        ///     Gets the previous record of IFormRepository sorted by primary key.
        /// </summary>
        /// <param name="primaryKey">The primary key column parameter used to find the previous record.</param>
        /// <returns>Returns a non-live, non-mapped instance of IFormRepository.</returns>
        Task<dynamic> GetPreviousAsync(object primaryKey);

        /// <summary>
        ///     Gets the next record of IFormRepository sorted by primary key.
        /// </summary>
        /// <param name="primaryKey">The primary key column parameter used to find the next record.</param>
        /// <returns>Returns a non-live, non-mapped instance of IFormRepository.</returns>
        Task<dynamic> GetNextAsync(object primaryKey);

        /// <summary>
        ///     Gets the last record of IFormRepository.
        /// </summary>
        /// <returns>Returns a non-live, non-mapped instance of IFormRepository.</returns>
        Task<dynamic> GetLastAsync();

        /// <summary>
        ///     Returns multiple instances of the IFormRepository against primary keys.
        /// </summary>
        /// <param name="primaryKeys">Array of primary key column parameter used on where filter.</param>
        /// <returns>Returns a non-live, non-mapped collection of IFormRepository.</returns>
        Task<IEnumerable<dynamic>> GetAsync(object[] primaryKeys);

        /// <summary>
        ///     Custom fields are user defined form elements for IFormRepository.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection for IFormRepository.</returns>
        Task<IEnumerable<CustomField>> GetCustomFieldsAsync(string resourceId);

        /// <summary>
        ///     Display fields provide a minimal id/value context for data binding IFormRepository.
        /// </summary>
        /// <returns>Returns an enumerable id and value collection for IFormRepository.</returns>
        Task<IEnumerable<DisplayField>> GetDisplayFieldsAsync();

        /// <summary>
        ///     Lookup fields provide a minimal code/value context for data binding IFormRepository.
        /// </summary>
        /// <returns>Returns an enumerable code and value collection for IFormRepository.</returns>
        Task<IEnumerable<DisplayField>> GetLookupFieldsAsync();

        /// <summary>
        ///     Inserts the instance of dynamic class to IFormRepository.
        /// </summary>
        /// <param name="form">The dynamic IFormRepository class to insert or update.</param>
        /// <param name="customFields">The custom field collection.</param>
        Task<object> AddOrEditAsync(Dictionary<string, object> form, List<CustomField> customFields);

        /// <summary>
        ///     Inserts the instance of dynamic class to IFormRepository.
        /// </summary>
        /// <param name="form">The instance of IFormRepository class to insert.</param>
        /// <param name="customFields">The custom field collection.</param>
        /// <param name="skipPrimaryKey">When skipped, the API will ignore primary key value on insert.</param>
        Task<object> AddAsync(Dictionary<string, object> form, List<CustomField> customFields, bool skipPrimaryKey);

        /// <summary>
        ///     Inserts or updates multiple instances of dynamic class to IFormRepository.;
        /// </summary>
        /// <param name="forms">List of IFormRepository instances to import.</param>
        /// <returns>Returns list of inserted object ids.</returns>
        Task<List<object>> BulkImportAsync(List<Dictionary<string, object>> forms);


        /// <summary>
        ///     Updates IFormRepository with an instance of dynamic class against the primary key value.
        /// </summary>
        /// <param name="form">The instance of IFormRepository class to update.</param>
        /// <param name="primaryKey">The value of the primary key which will be updated.</param>
        /// <param name="customFields">The custom field collection.</param>
        Task UpdateAsync(Dictionary<string, object> form, object primaryKey, List<CustomField> customFields);

        /// <summary>
        ///     Deletes the item from  IFormRepository against the primary key value.
        /// </summary>
        /// <param name="primaryKey">The value of the primary key which will be deleted.</param>
        Task DeleteAsync(object primaryKey);

        /// <summary>
        ///     Produces a paginated result of 50 IFormRepository classes.
        /// </summary>
        /// <returns>Returns the first page of collection of IFormRepository class.</returns>
        Task<IEnumerable<dynamic>> GetPaginatedResultAsync();

        /// <summary>
        ///     Produces a paginated result of 50 IFormRepository classes.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the paginated result.</param>
        /// <returns>Returns collection of IFormRepository class.</returns>
        Task<IEnumerable<dynamic>> GetPaginatedResultAsync(long pageNumber);

        Task<IEnumerable<Filter>> GetFiltersAsync(string database, string filterName);

        /// <summary>
        ///     Performs a filtered count on IFormRepository.
        /// </summary>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns number of rows of IFormRepository class using the filter.</returns>
        Task<long> CountWhereAsync(List<Filter> filters);

        /// <summary>
        ///     Performs a filtered pagination against IFormRepository producing result of 50 items.
        /// </summary>
        /// <param name="pageNumber">
        ///     Enter the page number to produce the paginated result. If you provide a negative number, the
        ///     result will not be paginated.
        /// </param>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns collection of IFormRepository class.</returns>
        Task<IEnumerable<dynamic>> GetWhereAsync(long pageNumber, List<Filter> filters);

        /// <summary>
        ///     Performs a filtered count on IFormRepository.
        /// </summary>
        /// <param name="filterName">The named filter.</param>
        /// <returns>Returns number of IFormRepository class using the filter.</returns>
        Task<long> CountFilteredAsync(string filterName);

        /// <summary>
        ///     Gets a filtered result of IFormRepository producing a paginated result of 50.
        /// </summary>
        /// <param name="pageNumber">
        ///     Enter the page number to produce the paginated result. If you provide a negative number, the
        ///     result will not be paginated.
        /// </param>
        /// <param name="filterName">The named filter.</param>
        /// <returns>Returns collection of IFormRepository class.</returns>
        Task<IEnumerable<dynamic>> GetFilteredAsync(long pageNumber, string filterName);
    }
}