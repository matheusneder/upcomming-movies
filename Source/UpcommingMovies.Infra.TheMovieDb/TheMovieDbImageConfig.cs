namespace UpcommingMovies.Infra.TheMovieDb
{
    /// <summary>
    /// TMDb service image configuration. This configuration is retrived from TMDb service.
    /// </summary>
    public class TheMovieDbImageConfig
    {
        /// <summary>
        /// BaseUrl for images.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// BaseUrl for images over https.
        /// </summary>
        public string SecureBaseUrl { get; set; }

        /// <summary>
        /// List of available sizes for backdrop images.
        /// </summary>
        public TheMovieDbImageSizeDescriptor[] BackdropSizes { get; set; }

        /// <summary>
        /// List of available sizes for poster images.
        /// </summary>
        public TheMovieDbImageSizeDescriptor[] PosterSizes { get; set; }
    }
}
