using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UpcommingMovies.Core.Domain.Models;
using UpcommingMovies.Core.Domain.Services;
using UpcommingMovies.Infra.TheMovieDb.Exceptions;

namespace UpcommingMovies.Infra.TheMovieDb.Services
{
    public class MovieDiscoverService : TheMovieDbServiceBase, IMovieDiscoverService
    {
        public MovieDiscoverService(TheMovieDbConfig theMovieDbConfig) : base(theMovieDbConfig)
        {
            
        }

        private TheMovieDbImageSizeDescriptor[] ParseImageSizeDescriptors(JArray sizeDescriptors)
        {
            return sizeDescriptors.Select(i => new TheMovieDbImageSizeDescriptor()
            {
                PathSegment = (string)i,
                Width = SafeConvertToWidth((string)i)
            }).ToArray();
        }

        // It's expected a "wXX" string, where XX is the width. So will try to extract the XX part and return as int if ok, return null otherwise.
        private int? SafeConvertToWidth(string value)
        {
            return int.TryParse(
                Regex.Match(value, "^w([0-9]+)$").Groups?[1]?.Value, out int result) 
                ? result 
                : (int?)null;
        }

        public async Task<TheMovieDbImageConfig> RetrieveConfigurationAsync()
        {
            var jsonResult = await GetAsync("/configuration", supressLanguage: true);

            try
            {
                var jObject = JObject.Parse(jsonResult);
                var jImageConf = jObject["images"];

                if(jImageConf != null)
                {
                    var imageConfig = new TheMovieDbImageConfig()
                    {
                        BaseUrl = (string)jImageConf["base_url"],
                        SecureBaseUrl = (string)jImageConf["secure_base_url"],
                        BackdropSizes = ParseImageSizeDescriptors(jImageConf["backdrop_sizes"] as JArray),
                        PosterSizes = ParseImageSizeDescriptors(jImageConf["poster_sizes"] as JArray)
                    };

                    return imageConfig;
                }
                else
                {
                    throw new ParseResponseException("Could not find images item on jsonResponse.");
                }
            }
            catch (Exception e)
            {
                throw new ParseResponseException("Error while parsing the response, see innerException for detail.", e);
            }
        }

        public async Task<IEnumerable<Genre>> RetrieveGengersAsync()
        {
            var jsonResult = await GetAsync("/genre/movie/list");

            try
            {
                var jObject = JObject.Parse(jsonResult);
                var genres = new List<Genre>();

                foreach (var jGenre in jObject["genres"])
                {
                    genres.Add(new Genre()
                    {
                        Id = (long)jGenre["id"],
                        Name = (string)jGenre["name"]
                    });
                }

                return genres;
            }
            catch (Exception e)
            {
                throw new ParseResponseException("Error while parsing the response, see innerException for detail.", e);
            }
        }

        private IEnumerable<Genre> genresCache;
        private CultureInfo genresCacheLanguage;

        public async Task<DiscoverResult> RetrieveUpCommingMoviesAsync(int page)
        {
            Task<IEnumerable<Genre>> genresCacheTask = null;
            Task<TheMovieDbImageConfig> imageConfigCacheTask = null;

            if (genresCache == null || genresCacheLanguage != CultureInfo.CurrentCulture)
            {
                genresCacheLanguage = CultureInfo.CurrentCulture;
                genresCacheTask = RetrieveGengersAsync(); // will hold for result later
            }

            if (imageConfigCache == null)
            {
                imageConfigCacheTask = RetrieveConfigurationAsync(); // will hold for result later
            }

            var jsonResult = await GetAsync("/discover/movie", new Dictionary<string, string>()
            {
                { "primary_release_date.gte", DateTimeOffset.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) },
                //{ "sort_by", "primary_release_date.asc" },
                { "page", page.ToString() },
            });

            try
            {
                if(genresCacheTask != null)
                {
                    genresCache = await genresCacheTask; 
                }

                if(imageConfigCacheTask != null)
                {
                    imageConfigCache = await imageConfigCacheTask;
                }

                var jObject = JObject.Parse(jsonResult);
                var result = new DiscoverResult()
                {
                    Page = (long)jObject["page"],
                    TotalPages = (long)jObject["total_results"],
                    TotalResults = (long)jObject["total_pages"]
                };

                var movies = new List<Movie>();

                foreach (var jMovie in jObject["results"])
                {
                    var genres = new List<Genre>();

                    foreach (long genreId in jMovie["genre_ids"])
                    {
                        var genre = genresCache.Where(g => g.Id == genreId).SingleOrDefault();

                        if (genre != null)
                        {
                            genres.Add(genre);
                        }
                    }

                    movies.Add(new Movie()
                    {
                        Id = (long)jMovie["id"],
                        Title = (string)jMovie["title"],
                        Overview = (string)jMovie["overview"],
                        ReleaseDate = DateTimeOffset.Parse((string)jMovie["release_date"]),
                        Genres = genres,
                        BuildBackdropImageUri = (forWidth) =>
                            BuildImageUri(imageConfigCache.BackdropSizes, forWidth, (string)jMovie["backdrop_path"]),
                        BuildPosterImageUri = (forWidth) =>
                            BuildImageUri(imageConfigCache.PosterSizes, forWidth, (string)jMovie["poster_path"])
                    });
                }

                result.Movies = movies;

                return result;
            }
            catch (Exception e)
            {
                throw new ParseResponseException("Error while parsing the response bady, see innerException for detail.", e);
            }
        }

        private Uri BuildImageUri(TheMovieDbImageSizeDescriptor[] imageSizeDescriptors, int width, string path)
        {
            Uri result = null;

            if (!string.IsNullOrEmpty(path))
            {
                var sizePathSegment = BestImageSizePathSegmentForWidth(imageConfigCache.BackdropSizes, width);
                result = new Uri($"{imageConfigCache.SecureBaseUrl}{sizePathSegment}{path}");
                Debug.WriteLine($"Built new image uri: {result.OriginalString}");
            }

            return result;
        }

        TheMovieDbImageConfig imageConfigCache;

        private string BestImageSizePathSegmentForWidth(TheMovieDbImageSizeDescriptor[] imageSizeDescriptors, int width)
        {
            return imageSizeDescriptors
                .Where(s => s.Width > width || !s.Width.HasValue)
                .OrderBy(s => s.Width ?? int.MaxValue) // null widths on the end of result
                .Select(s => s.PathSegment)
                .FirstOrDefault();
        }
    }
}
