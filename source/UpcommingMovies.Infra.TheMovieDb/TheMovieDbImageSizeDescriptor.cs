namespace UpcommingMovies.Infra.TheMovieDb
{
    /// <summary>
    /// Descript an available image size and its Width as integer 
    /// value and the PathSegment used to build images url.
    /// </summary>
    public class TheMovieDbImageSizeDescriptor
    {
        public int? Width { get; set; }
        public string PathSegment { get; set; }
    }
}