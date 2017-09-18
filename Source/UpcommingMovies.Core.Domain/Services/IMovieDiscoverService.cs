using System.Threading.Tasks;
using UpcommingMovies.Core.Domain.Models;

namespace UpcommingMovies.Core.Domain.Services
{
    /// <summary>
    /// Movie dicover service contract
    /// </summary>
    public interface IMovieDiscoverService
    {
        /// <summary>
        /// Retrive a list of upcomming movies (with release date greater then current date).
        /// </summary>
        /// <param name="page">The page number to retrive.</param>
        /// <returns>
        /// The list of upcomming movies wrapped as a <see cref="DiscoverResult"/>.
        /// The result type is Task because this is an async method and it must used with await operator.
        /// </returns>
        Task<DiscoverResult> RetrieveUpCommingMoviesAsync(int page);
    }
}
