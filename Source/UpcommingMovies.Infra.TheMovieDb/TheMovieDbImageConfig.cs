namespace UpcommingMovies.Infra.TheMovieDb
{
    public class TheMovieDbImageConfig
    {
        public string BaseUrl { get; set; }
        public string SecureBaseUrl { get; set; }
        public TheMovieDbImageSizeDescriptor[] BackdropSizes { get; set; }
        public TheMovieDbImageSizeDescriptor[] PosterSizes { get; set; }
    }
}
