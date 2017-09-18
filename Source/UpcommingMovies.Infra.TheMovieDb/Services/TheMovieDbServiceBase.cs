using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UpcommingMovies.Infra.TheMovieDb.Exceptions;

namespace UpcommingMovies.Infra.TheMovieDb.Services
{
    public abstract class TheMovieDbServiceBase
    {
        private readonly TheMovieDbConfig _movieDbConfig;
        private readonly HttpClient _httpClient;
        private readonly string _baseUriWithEndSlashFree;

        public TheMovieDbServiceBase(TheMovieDbConfig theMovieDbConfig)
        {
            _movieDbConfig = theMovieDbConfig;
            _baseUriWithEndSlashFree = theMovieDbConfig.ApiBaseUri.ToString().TrimEnd('/');
            _httpClient = new HttpClient();
        }


        // Build the url including api_key, language and custom query parameters.
        // api_key is taken from TheMovieDbConfig.ApiKey and language from CultureInfo.CurrentCulture.Name
        private Uri BuildUri(string relativePath, IDictionary<string, string> query, bool supressLanguage)
        {
            if(query == null)
            {
                query = new Dictionary<string, string>();
            }

            query["api_key"] = _movieDbConfig.ApiKey;

            if (!supressLanguage)
            {
                query["language"] = CultureInfo.CurrentCulture?.Name ?? "en-US";
            }

            string queryString = string.Join("&", 
                    query.Select(q => string.Join("=", Uri.EscapeUriString(q.Key), Uri.EscapeUriString(q.Value))));

            string relativePathWithStartSlashFree = relativePath.TrimStart('/');

            return new Uri($"{_baseUriWithEndSlashFree}/{relativePathWithStartSlashFree}?{queryString}");
        }

        /// <summary>
        /// Perform a get request for a specific relative path and query parameters. Is not necessary to supply api_key and language as query parameters.
        /// </summary>
        /// <param name="relativePath">The relative path to resource (without baseUri part).</param>
        /// <param name="query">
        /// A dictionary with parameter name as key and value to build the query string. 
        /// api_key and language values are not necessary and should not supplied.
        /// If supplied, they will be overridden by TheMovieDbConfig.ApiKey and CultureInfo.CurrentCulture.Name.
        /// </param>
        /// <param name="supressLanguage">If true it will not include language parameter on request final url querystring.</param>
        /// <returns>Raw string response body if reponse status code is Ok (200 family)</returns>
        /// <exception cref="ResourceUnauthorizedException">
        /// Response gives a 401 (unauthorized) http status. This means that the api_key is not no longer valid or invalid.
        /// </exception>
        /// <exception cref="ResourceNotFoundException">
        /// Response gives a 404 (not found) http status. Means that the request no longer exists, or the relativePath or baseUri is wrong.
        /// </exception>
        /// <exception cref="TooManyRequestsException">
        /// Response gives a 429 (too many requests) http status. Means that the client did too much requests, 
        /// take a look at themoviedb request rate limiting at https://developers.themoviedb.org/3/getting-started/request-rate-limiting 
        /// </exception>
        /// <exception cref="RemoteServiceException">
        /// Response gives any http status but 200 family, 401, 404 or 429.
        /// </exception>
        protected async Task<string> GetAsync(string relativePath, IDictionary<string, string> query = null, bool supressLanguage = false)
        {
            var uri = BuildUri(relativePath, query, supressLanguage);
            Debug.WriteLine($"Performing get request to: {uri.OriginalString}");
            HttpResponseMessage response = null;

            try
            {
                response = await _httpClient.GetAsync(uri);
            }
            catch (Exception e)
            {
                throw new RemoteServiceException(uri, e);
            }

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    throw new TransportException(uri, e);
                }
            }


            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new ResourceUnauthorizedException(uri, response);
                case HttpStatusCode.NotFound:
                    throw new ResourceNotFoundException(uri, response);
                case (HttpStatusCode)429: // too many requests
                    throw new TooManyRequestsException(uri, response);
                default:
                    throw new RemoteServiceException(uri, response);
            }

        }
    }
}
