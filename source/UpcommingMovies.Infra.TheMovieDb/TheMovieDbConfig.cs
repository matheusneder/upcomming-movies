using System;

namespace UpcommingMovies.Infra.TheMovieDb
{
    /// <summary>
    /// TMDb services credential and configuration.
    /// </summary>
    public class TheMovieDbConfig
    {
        /// <summary>
        /// TMDb services apiKey.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// TMDb services base uri.
        /// </summary>
        public Uri ApiBaseUri { get; set; }
    }
}
