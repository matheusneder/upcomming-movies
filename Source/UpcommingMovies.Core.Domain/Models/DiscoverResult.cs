using System.Collections.Generic;

namespace UpcommingMovies.Core.Domain.Models
{
    public class DiscoverResult
    {
        public long TotalResults { get; set; }
        public long TotalPages { get; set; }
        public long Page { get; set; }
        public virtual IEnumerable<Movie> Movies { get; set; }
    }
}
