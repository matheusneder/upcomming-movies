using System.Collections.Generic;

namespace UpcommingMovies.Core.Domain.Models
{
    /// <summary>
    /// Represents a retrived movie list page.
    /// </summary>
    public class DiscoverResult
    {
        /// <summary>
        /// The total number of results for entire dataset.
        /// </summary>
        public long TotalResults { get; set; }

        /// <summary>
        /// Total number of pages for entire dataset.
        /// </summary>
        public long TotalPages { get; set; }

        /// <summary>
        /// Page number for current page.
        /// </summary>
        public long Page { get; set; }

        /// <summary>
        /// The list of movie.
        /// </summary>
        public virtual IEnumerable<Movie> Movies { get; set; }
    }
}
