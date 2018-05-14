using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.DataAccess.Models;

namespace Frapid.WebApi.DataAccess
{
    public interface IViewRepository
    {
        /// <summary>
        ///     Performs count on IViewRepository.
        /// </summary>
        /// <returns>Returns the number of IViewRepository.</returns>
        Task<long> CountAsync();

        /// <summary>
        ///     Return all instances of the rows from IViewRepository.
        /// </summary>
        /// <returns>Returns a non-live, non-mapped instances of rows.</returns>
        Task<IEnumerable<dynamic>> GetAsync();

        /// <summary>
        ///     Display fields provide a minimal id/value context for data binding IViewRepository.
        /// </summary>
        /// <returns>Returns an enumerable id and value collection for IViewRepository.</returns>
        Task<IEnumerable<DisplayField>> GetDisplayFieldsAsync();

        /// <summary>
        ///     Display fields provide a minimal id/value context for data binding IViewRepository.
        /// </summary>
        /// <returns>Returns an enumerable id and value collection for IViewRepository.</returns>
        Task<IEnumerable<DisplayField>> GetLookupFieldsAsync();

        /// <summary>
        ///     Produces a paginated result of 50 items from IViewRepository.
        /// </summary>
        /// <returns>Returns the first page of collection of rows.</returns>
        Task<IEnumerable<dynamic>> GetPaginatedResultAsync();

        /// <summary>
        ///     Produces a paginated result of 50 items from IViewRepository.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the paginated result.</param>
        /// <returns>Returns collection of rows.</returns>
        Task<IEnumerable<dynamic>> GetPaginatedResultAsync(long pageNumber);

        Task<IEnumerable<Filter>> GetFiltersAsync(string database, string filterName);

        /// <summary>
        ///     Performs a filtered count on IViewRepository.
        /// </summary>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns number of rows of rows using the filter.</returns>
        Task<long> CountWhereAsync(List<Filter> filters);

        /// <summary>
        ///     Produces a paginated result of 50 items using the supplied filters from IViewRepository.
        /// </summary>
        /// <param name="pageNumber">
        ///     Enter the page number to produce the paginated result. If you provide a negative number, the
        ///     result will not be paginated.
        /// </param>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns collection of rows.</returns>
        Task<IEnumerable<dynamic>> GetWhereAsync(long pageNumber, List<Filter> filters);

        /// <summary>
        ///     Performs a filtered count on IViewRepository.
        /// </summary>
        /// <param name="filterName">The named filter.</param>
        /// <returns>Returns number of rows of rows using the filter.</returns>
        Task<long> CountFilteredAsync(string filterName);

        /// <summary>
        ///     Produces a paginated result of 50 items using the supplied filter name from IViewRepository.
        /// </summary>
        /// <param name="pageNumber">
        ///     Enter the page number to produce the paginated result. If you provide a negative number, the
        ///     result will not be paginated.
        /// </param>
        /// <param name="filterName">The named filter.</param>
        /// <returns>Returns collection of rows.</returns>
        Task<IEnumerable<dynamic>> GetFilteredAsync(long pageNumber, string filterName);
    }
}