using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpcommingMovies.Infra.TheMovieDb.Services;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Linq;
using HttpMock;

namespace UpcommingMovies.Infra.TheMovieDb.Test
{
    [TestClass]
    public class MovieDiscoverServiceTest
    {
        private readonly IHttpServer _stubHttp;
        private const string _httpMockBaseUri = "http://localhost:9979";

        public MovieDiscoverServiceTest()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            _stubHttp = HttpMockRepository.At(_httpMockBaseUri);
            _stubHttp.Stub(x => x.Get("/discover/movie"))
                .Return(TheMovieDbMockResponseData.MoviesData)
                .OK();
            _stubHttp.Stub(x => x.Get("/genre/movie/list"))
                .Return(TheMovieDbMockResponseData.GenresData)
                .OK();
            _stubHttp.Stub(x => x.Get("/configuration"))
                .Return(TheMovieDbMockResponseData.ConfigurationData)
                .OK();
        }

        private MovieDiscoverService _movieDiscoverService = new MovieDiscoverService(new TheMovieDbConfig()
        {
            ApiKey = "XYZ",
            ApiBaseUri = new Uri(_httpMockBaseUri)
        });

        [TestMethod]
        public async Task TestRetrieveUpCommingMovies()
        {
            var upcommingMovies = await _movieDiscoverService.RetrieveMoviesAsync(1);
            var firstMovie = upcommingMovies.Movies.First();

            // based on static data from TheMovieDbMockRespostaData class
            Assert.AreEqual(firstMovie.Title, "Deadpool");
            Assert.AreEqual(firstMovie.Genres.First().Name, "Action");
            Assert.AreEqual(firstMovie.BuildPosterImageUri(90).AbsoluteUri, "https://image.tmdb.org/t/p/w92/inVq3FRqcYIRl2la8iZikYYxFNR.jpg");
        }

        [TestMethod]
        public async Task TestRetrieveGengers()
        {
            var genders = await _movieDiscoverService.RetrieveGengersAsync();

            // based on static data from TheMovieDbMockRespostaData class
            Assert.AreEqual(genders.First().Name, "Action");
        }

        [TestMethod]
        public async Task TestRetrieveConfiguration()
        {
            var conf = await _movieDiscoverService.RetrieveConfigurationAsync();

            // based on static data from TheMovieDbMockRespostaData class
            Assert.AreEqual(conf.PosterSizes.First().PathSegment, "w92");
        }

    }
}
