namespace UpcommingMovies.Core.Domain.Models
{
    /// <summary>
    /// Genre entity.
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// The genre identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The genre name.
        /// </summary>
        public string Name { get; set; }
    }
}