using System;
using System.Collections.Generic;

namespace UpcommingMovies.Core.Domain.Models
{
    /// <summary>
    /// Movie entity.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// The movie identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Movie title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Movie overview
        /// </summary>
        public string Overview { get; set; }

        /// <summary>
        /// The release date of movie.
        /// </summary>
        public DateTimeOffset ReleaseDate { get; set; }

        /// <summary>
        /// List of genres.
        /// </summary>
        public IEnumerable<Genre> Genres { get; set; }

        /// <summary>
        /// Delegate to be filled with a function responsible to produce 
        /// the image uri for movie poster image based on a supplied width.
        /// </summary>
        public Func<int, Uri> BuildPosterImageUri { get; set; }

        /// <summary>
        /// Delegate to be filled with a function responsible to produce 
        /// the image uri for movie backdrop image based on a supplied width.
        /// </summary>
        public Func<int, Uri> BuildBackdropImageUri { get; set; }
    }
}
