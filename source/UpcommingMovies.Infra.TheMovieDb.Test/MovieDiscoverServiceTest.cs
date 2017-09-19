using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpcommingMovies.Infra.TheMovieDb.Services;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Linq;

namespace UpcommingMovies.Infra.TheMovieDb.Test
{
    [TestClass]
    public class MovieDiscoverServiceTest
    {
        public MovieDiscoverServiceTest()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
        }

        private MovieDiscoverService _movieDiscoverService = new MovieDiscoverService(new TheMovieDbConfig()
        {
            ApiKey = "25ac607028f4f8915c576ead4be555df",
            ApiBaseUri = new Uri("https://api.themoviedb.org/3/")
        });

        [TestMethod]
        public async Task TestRetrieveUpCommingMovies()
        {
            var upcomming = await _movieDiscoverService.RetrieveMoviesAsync(1);
            var x = upcomming.Movies.Select(m => m.BuildBackdropImageUri(500)).ToArray();
        }

        [TestMethod]
        public async Task TestRetrieveGengers()
        {
            var genders = await _movieDiscoverService.RetrieveGengersAsync();
        }

        [TestMethod]
        public async Task TestRetrieveConfiguration()
        {
            var conf = await _movieDiscoverService.RetrieveConfigurationAsync();
        }

    }
}
