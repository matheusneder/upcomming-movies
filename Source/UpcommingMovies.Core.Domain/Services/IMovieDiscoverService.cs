using System;
using System.Threading.Tasks;
using UpcommingMovies.Core.Domain.Models;

namespace UpcommingMovies.Core.Domain.Services
{
    public interface IMovieDiscoverService
    {
        Task<DiscoverResult> RetrieveUpCommingMoviesAsync(int page);
    }
}
